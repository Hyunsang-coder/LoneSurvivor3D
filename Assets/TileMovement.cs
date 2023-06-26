using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float snapSize = 100f;
    void Awake()
    {
        player = Player.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.tag.Contains("MapDetector")) return;
        Vector3 playerPos = player.transform.position;
        Vector3 tilePos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - tilePos.x);
        float diffZ = Mathf.Abs(playerPos.z - tilePos.z);

        float dirX = playerPos.x - tilePos.x < 0 ? -1 : 1;
        float dirZ = playerPos.z - tilePos.z < 0 ? -1 : 1;


        if (diffX > diffZ){
            transform.Translate(Vector3.right * dirX * snapSize);
        }

        else if (diffX <= diffZ)
        {
            transform.Translate(Vector3.forward * dirZ * snapSize);
        }

        Debug.Log("DiffX:" + diffX + ", DiffZ:" + diffZ);
    }
}
