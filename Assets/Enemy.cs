using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyindex;
    Player player;
    Rigidbody rigid;

    public float speed = 0.005f;
    void Awake()
    {
        player = Player.Instance;
        rigid = GetComponent<Rigidbody>();
    } 

    void FixedUpdate()
    {
        transform.LookAt(player.transform.position);
        Vector3 dir = player.transform.position - rigid.position;
        Vector3 nextPos = dir.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextPos);
    }
}