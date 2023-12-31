using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    PlayerMovement player;
    Transform target;

    float spawnTimer;
    int spawnCount;
    int maxSpawnCount = 20;

    [SerializeField] float spawnTimerMax = 5f;
   
    void Start()
    {
        target = PlayerMovement.Instance.gameObject.transform;
        spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnCount > maxSpawnCount)
        {
            return; 
        }
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0;
            SpawnEnmey(0);
            spawnCount++;
        }
    }

    private void LateUpdate() {
        transform.position = target.position;
    }

    void SpawnEnmey(int index)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = PoolManager.Instance.GetObject(index);
            enemy.transform.position = spawnPoints[i].position + new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3));
        }
        
    }
}
