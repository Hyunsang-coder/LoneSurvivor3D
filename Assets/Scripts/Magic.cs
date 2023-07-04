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

        fireBall.transform.position = transform.position + new Vector3(1f, 1f, 0f);

        
        fireBall.transform.rotation = Quaternion.LookRotation(transform.forward);

    }
}
