using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Animator animator;

    
    public Material dissolveMaterial;
    public Material originaMaterial;
    SkinnedMeshRenderer smRenderer;

    public float duration = 2f;

    Player player;
    Rigidbody rigid;
    HealthSystem healthSystem;

    Vector3 dir;

    public float speed = 2;

    private Vector3 lastPosition;
    private bool isMoving;
    bool isDead;

    public Transform target;




    Collider enemyCollider;
    void OnEnable()
    {
        player = Player.Instance;
        rigid = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystem>();

        animator = GetComponent<Animator>();
        target = null;

        enemyCollider = GetComponent<Collider>();

        smRenderer = transform.Find("Character").GetComponent<SkinnedMeshRenderer>();

        smRenderer.material = new Material(dissolveMaterial);

        StartCoroutine(SpawnEffect());

        isInbound = false;

        
    }

    private void OnDisable() {

        smRenderer.material = new Material(originaMaterial);
    }

   
    private void Update()
    {
        if (target)
        {
            dir = target.position - rigid.position;
        }
        

        if (isDead)
        {
            animator.SetBool("Move", false);
            isMoving = false;
            target = null;
        } 

        if (target && !isMoving && !isDead)
        {
            animator.SetBool("Move", true);
            isMoving = true;
        }
        else if (!target && isMoving && !isDead)
        {
            animator.SetBool("Move", false);
            isMoving = false;
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

        GetComponent<CapsuleCollider>().isTrigger = false;
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

        GetComponent<CapsuleCollider>().isTrigger = true;
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

    public bool isInbound;

    

    void FixedUpdate()
    {
        if (isDead || isInbound)
        {
            rigid.velocity = Vector3.zero;
            return;
        }

        
        if (target && !isDead)
        {
            transform.LookAt(target.position);
            
            Vector3 nextPos = dir.normalized * speed * Time.fixedDeltaTime;

            rigid.MovePosition(rigid.position + nextPos);
        }
        
    }


    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(10f * Time.deltaTime );
        }

        if (other.CompareTag("CharacterBounds"))
        {
            isInbound = true;
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

        if (other.CompareTag("CharacterBounds"))
        {
            isInbound = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.CompareTag("CharacterBounds"))
        {
            isInbound = false;
        }
    }




    public void SetTarget(Transform target)
    {
        this.target  = target;
    }
}