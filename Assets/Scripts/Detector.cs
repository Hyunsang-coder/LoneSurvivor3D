using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    private void Awake() {
        
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            enemy.SetTarget(other.gameObject.transform);
            Debug.Log("Found a target!");
        }
    }
}
