using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Transform spawnPoint;
    public float speed = 10f;
    
    Vector3 forwardDirection;
    public TrailRenderer trail; 
    
    public float lifeTime = 4f;
    float timer;
    private void OnEnable() {
        
        timer = 0;
        // 활성화되는 순간 player의 정면 기준으로 forward direction 설정
        forwardDirection = PlayerMovement.Instance.transform.forward;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            timer = 0;
            gameObject.SetActive(false);
        }

    }

    private void FixedUpdate() {

        transform.Translate(forwardDirection * Time.fixedDeltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Contains("Enemy"))
        {
            gameObject.SetActive(false);    
            //trail.enabled = false;
        }
        
    }

    void OnDisable()
    {
        trail.Clear();
    }
}
    
