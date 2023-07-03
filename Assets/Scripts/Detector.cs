using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;

    private void Awake() {
        
        enemy = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            enemy.SetTarget(other.gameObject.transform);
            //Debug.Log("Found a target!");
        }
    }
}
