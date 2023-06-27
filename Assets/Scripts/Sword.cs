using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] Transform target;

    [SerializeField] float pushBack = 3f;
    
    private void OnEnable() {
        
    }

    private void Start() {
        target = Player.Instance.gameObject.transform;
    }
    void Update()
    {
        transform.Rotate(0, 90f * Time.deltaTime * speed, 0, Space.World);
    }

    private void LateUpdate() {
        transform.position = target.position;
    }

    public float GetPushBack()
    {
        return pushBack;
    }
}
