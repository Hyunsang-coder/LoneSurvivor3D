using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    [SerializeField] Transform player;
   
    
    // Start is called before the first frame update
    void Start()
    {
         player = PlayerMovement.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMovement>().SetTarget(PlayerMovement.Instance.transform);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMovement>().SetTarget(PlayerMovement.Instance.transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMovement>().SetTarget(null);
        }
        
    }
}
