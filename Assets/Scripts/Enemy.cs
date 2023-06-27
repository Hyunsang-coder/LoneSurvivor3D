using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Animator animator;

    /*
    public Material material;
    public Color hitColor = Color.red;
    public float hitDuration = 0.3f;
    Color originalColor = Color.white;
    */

    Player player;
    Rigidbody rigid;
    HealthSystem healthSystem;

    Vector3 dir;

    public float speed = 2;

    private Vector3 lastPosition;
    private bool isMoving;


    void OnEnable()
    {
        player = Player.Instance;
        rigid = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystem>();

        animator = GetComponent<Animator>();

        //material = GetComponent<MeshRenderer>().material;
        //material.color = originalColor;

    }

   
    private void Update()
    {
       if (player)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }


    void FixedUpdate()
    {
        transform.LookAt(player.transform.position);
        dir = player.transform.position - rigid.position;
        Vector3 nextPos = dir.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextPos);
        
    }


    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(10f * Time.deltaTime );
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet"))
        {
            healthSystem.TakeDamage(5f);

            float pushBack = other.transform.parent.GetComponent<Sword>().GetPushBack();
            rigid.AddForce(-dir * pushBack, ForceMode.Impulse);

        }
    }


    /*
    void OnHitColorChanged()
    {   
        material.color = hitColor;

        if(!gameObject.activeSelf) return;
        StartCoroutine(ResetColor());

    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(hitDuration);

        material.color = originalColor;
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
    */
}