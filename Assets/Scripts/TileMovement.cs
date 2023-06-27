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
       
        if (other.tag.Contains("Tile"))
        {
            Vector3 playerPos = player.transform.position;
            Vector3 tilePos = other.transform.position;
            
            float diffX = Mathf.Abs(playerPos.x - tilePos.x);
            float diffZ = Mathf.Abs(playerPos.z - tilePos.z);

            float dirX = playerPos.x - tilePos.x < 0 ? -1 : 1;
            float dirZ = playerPos.z - tilePos.z < 0 ? -1 : 1;


            if (diffX > diffZ){
                other.transform.Translate(Vector3.right * dirX * snapSize);
            }

            else if (diffX <= diffZ)
            {
                other.transform.Translate(Vector3.forward * dirZ * snapSize);
            }

            //Debug.Log("DiffX:" + diffX + ", DiffZ:" + diffZ);
        }

        if (other.tag.Contains("Enemy"))
        {
            Vector3 originalPos = other.transform.position;
            Vector3 direction = (player.transform.position - other.transform.position).normalized;
            Vector3 newPos = other.transform.position += direction * 30 + new Vector3 (Random.Range(0, 4), 0, Random.Range(0, 4));

            Debug.Log("Original Pos:" + originalPos + "New Pos: " + newPos);

        }
        

        
    }
}
