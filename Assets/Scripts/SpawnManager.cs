using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    PlayerMovement player;

     float spawnTimer;
    [SerializeField] float spawnTimerMax = 5f;
    void Awake()
    {
        player = PlayerMovement.Instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0;
            SpawnEnmey(1);
        }
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
