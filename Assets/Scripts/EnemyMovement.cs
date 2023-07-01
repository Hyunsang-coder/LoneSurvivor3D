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

    [SerializeField] bool isMoving;
    [SerializeField] bool isDead;

  

    public Transform target;

    [SerializeField] bool isGrounded;
    [SerializeField] float rayLength = 0.2f;
    const float gravity = -9.81f;

    float glowTime = 0f;

    

    void OnEnable()
    {
        healthSystem = GetComponent<HealthSystem>();

        animator = GetComponent<Animator>();
        

        smRenderer = transform.Find("Character").GetComponent<SkinnedMeshRenderer>();

        smRenderer.material = new Material(dissolveMaterial);

        controller = GetComponent<CharacterController>();

        healthSystem.onDeath += Dead;

        StartCoroutine(SpawnEffect());
    }

    private void OnDisable() {

        smRenderer.material = new Material(originaMaterial);
    }

   
    private void Update()
    {
        
        if (isDead)
        {
            animator.SetBool("Run", false);
            isMoving = false;
            target = null;
        }
        else
        {
            if (target)
            {
                moveVector = (target.position - transform.position).normalized;
            }
            else
            {
                moveVector = Vector3.zero;
            }
    
            if (moveVector.magnitude > 0f && !isMoving)
            {
                animator.SetBool("Run", true);
                isMoving = true;
            }

            else if (moveVector.magnitude <= 0f)
            {
                animator.SetBool("Run", false);
                isMoving = false;
            }

        }
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, rayLength))
        {
            isGrounded = true;
            moveVector.y = 0;
        }
        else
        {
            isGrounded = false;
            moveVector.y += gravity;

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

    
    IEnumerator SpawnEffect()
    {
        isDead = true;
        target = null;
        isMoving = false;
        

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
        controller.enabled = true;
        animator.SetBool("Run", false);

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
        controller.enabled = false;

        Material currentMaterial = new Material(dissolveMaterial);
        float startTime = Time.time;
        glowTime = 2f;
        animator.SetTrigger("Dead");

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


    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag.Contains("Player"))
        {
            hit.gameObject.GetComponent<HealthSystem>().TakeDamage(10f * Time.deltaTime );

            if (!animator.IsInTransition(0))
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet"))
        {
            healthSystem.TakeDamage(5f);

            if (!animator.IsInTransition(0) && !isDead)
            {
                animator.SetTrigger("Hit");
            }
        }

    }



    public void SetTarget(Transform target)
    {
        this.target  = target;
    }
}
