using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject fireBall;
    float timer;
    float timerMax = 1f;
    private void Awake() {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            timer = 0;
            ThrowFireBall();
        }
    }

    public void ThrowFireBall()
    {
        fireBall = PoolManager.Instance.GetObject(1);

        // position은 point를 기준으로 맞춤
        fireBall.transform.position = transform.Find("Point").position;
        
        // 방향은 player를 기준으로 맞춤
        fireBall.transform.rotation = transform.rotation;

    }
}
