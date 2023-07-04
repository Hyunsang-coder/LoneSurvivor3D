using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Transform spawnPoint;
    public float speed = 10f;
    
    Vector3 forwardDirection;
    private void OnEnable() {
        
        forwardDirection = new Vector3(PlayerMovement.Instance.moveVector.x, 1f, PlayerMovement.Instance.moveVector.z);

        Debug.Log("forwarDirection:" + forwardDirection);
    }
    void Start()
    {
        
    }
    
    private void Update() 
    {
        if (spawnPoint == null) return;

        transform.Translate(forwardDirection * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Contains("Enemy"))
        {
            gameObject.SetActive(false);    
        }
        
    }
}
    
