using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyindex;
    Player player;
    Rigidbody rigid;
    HealthSystem healthSystem;

    public float speed = 0.005f;
    void Awake()
    {
        player = Player.Instance;
        rigid = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystem>();
    } 

    void FixedUpdate()
    {
        transform.LookAt(player.transform.position);
        Vector3 dir = player.transform.position - rigid.position;
        Vector3 nextPos = dir.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextPos);
    }


    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Bullet"))
        {
            healthSystem.TakeDamage(5f * Time.deltaTime );
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet"))
        {
            healthSystem.TakeDamage(5f);
        }
    }
}