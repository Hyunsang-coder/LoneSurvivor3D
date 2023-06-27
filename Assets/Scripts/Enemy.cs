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

    public Transform target;


    Collider enemyCollider;
    void OnEnable()
    {
        player = Player.Instance;
        rigid = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystem>();

        animator = GetComponent<Animator>();
        target = null;

        //material = GetComponent<MeshRenderer>().material;
        //material.color = originalColor;

        enemyCollider = GetComponent<Collider>();

    }

   
    private void Update()
    {
       if (target)
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
        if (target)
        {
            transform.LookAt(target.position);
            dir = target.position - rigid.position;
            Vector3 nextPos = dir.normalized * speed * Time.fixedDeltaTime;

            rigid.MovePosition(rigid.position + nextPos);
        }
        
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

            Debug.Log("Take Damage");
        }
    }




    public void SetTarget(Transform target)
    {
        this.target  = target;
    }
}