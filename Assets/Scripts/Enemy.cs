using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Animator animator;

    
    public Material deathMaterial;
    public Material originaMaterial;
    public float duration = 2f;

    Player player;
    Rigidbody rigid;
    HealthSystem healthSystem;

    Vector3 dir;

    public float speed = 2;

    private Vector3 lastPosition;
    private bool isMoving;

    public Transform target;

    SkinnedMeshRenderer smRenderer;


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

        originaMaterial = smRenderer.material;

        smRenderer.material = deathMaterial;
        
    }

   
    private void Update()
    {

       if (target && !isMoving)
        {
            animator.SetBool("Move", true);
            isMoving = true;
        }
        else if (!target && isMoving)
        {
            animator.SetBool("Move", false);
            isMoving = false;
        }

    }

    private void Start() {
        StartCoroutine(SpawnEffect());
        healthSystem.onDeath += Dead;
    }

    float glowTime = 0f;
    IEnumerator SpawnEffect()
    {
        // 값이 공유되지 않게 새로운 material 인스턴스 만들기 
        Material currentMaterial = new Material(smRenderer.material);
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            glowTime += Time.deltaTime;
            currentMaterial.SetFloat("_value", glowTime);
            smRenderer.material = currentMaterial;

            yield return null;
        }

        smRenderer.material = new Material(originaMaterial);
    }

    public void Dead()
    {
        StartCoroutine(DeathEffect());
    }
    IEnumerator DeathEffect()
    {
        // 값이 공유되지 않게 새로운 material 인스턴스 만들기 
        Material currentMaterial = new Material(deathMaterial);
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            glowTime = 2f;
            glowTime -= Time.deltaTime;
            currentMaterial.SetFloat("_value", glowTime);
            smRenderer.material = currentMaterial;

            yield return null;
        }

        smRenderer.material = new Material(originaMaterial);
        gameObject.SetActive(false);
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