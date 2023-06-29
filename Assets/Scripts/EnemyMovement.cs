using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Animator animator;

    
    public Material dissolveMaterial;
    public Material originaMaterial;
    SkinnedMeshRenderer smRenderer;

    CharacterController controller;
    public float duration = 2f;

    HealthSystem healthSystem;

    public Vector3 moveVector;

    public float speed = 10;

    private Vector3 lastPosition;
    private bool isMoving;
    bool isDead;

    public Transform target;




    void OnEnable()
    {
        healthSystem = GetComponent<HealthSystem>();

        animator = GetComponent<Animator>();
        

        smRenderer = transform.Find("Character").GetComponent<SkinnedMeshRenderer>();

        smRenderer.material = new Material(dissolveMaterial);

        controller = GetComponent<CharacterController>();

        target = null;

        StartCoroutine(SpawnEffect());

        
    }

    private void OnDisable() {

        smRenderer.material = new Material(originaMaterial);
    }

   
    private void Update()
    {
        
        if (isDead)
        {
            animator.SetBool("Move", false);
            isMoving = false;
            target = null;
        } 

        if (target)
        {
            moveVector = (target.position - transform.position).normalized;
        }


        if (moveVector.magnitude > 0 && !isMoving)
        {
            animator.SetBool("Move", true);
            isMoving = true;
        }

        else if (moveVector.magnitude == 0 )
        {
            animator.SetBool("Move", false);
            isMoving = false;
        }

    }


     void FixedUpdate()
    {
        if (isDead || !target) return;

        if (target)
        {
            controller.Move(moveVector * Time.fixedDeltaTime * speed);
            transform.LookAt(target.position);
        }
        
    }

    private void Start() {
        
        healthSystem.onDeath += Dead;
    }

    float glowTime = 0f;
    IEnumerator SpawnEffect()
    {
        isDead = true;

        Material currentMaterial = new Material(dissolveMaterial);
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            glowTime += Time.deltaTime;
            currentMaterial.SetFloat("_value", glowTime);
            smRenderer.material = currentMaterial;

            yield return null;
        }

        smRenderer.material = new Material(originaMaterial);

        isDead = false;

    }

    public void Dead()
    {
        StartCoroutine(DeathEffect());
    }
    IEnumerator DeathEffect()
    {
        // 값이 공유되지 않게 새로운 material 인스턴스 만들기 
        isDead = true;
        target = null;
        isMoving = false;

        Material currentMaterial = new Material(dissolveMaterial);
        float startTime = Time.time;
        glowTime = 2f;

        while (Time.time - startTime < duration)
        {
            
            smRenderer.material = currentMaterial;
            glowTime -= Time.deltaTime;
            currentMaterial.SetFloat("_value", glowTime);
            
             yield return null;
        }

        smRenderer.material = new Material(originaMaterial);
        gameObject.SetActive(false);
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

            Debug.Log("Take Damage");
        }

    }



    public void SetTarget(Transform target)
    {
        this.target  = target;
    }
}
