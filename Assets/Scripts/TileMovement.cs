using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float snapSize = 100f;
    void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
         player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }

    void OnTriggerExit(Collider other)
    {
       
        if (other.tag.Contains("Tile"))
        {
            Vector3 tilePos = other.transform.position;
            
            float diffX = Mathf.Abs(player.position.x - tilePos.x);
            float diffZ = Mathf.Abs(player.position.z - tilePos.z);

            float dirX = player.position.x - tilePos.x < 0 ? -1 : 1;
            float dirZ = player.position.z - tilePos.z < 0 ? -1 : 1;


            if (diffX > diffZ){
                other.transform.Translate(Vector3.right * dirX * snapSize);
            }

            else if (diffX <= diffZ)
            {
                other.transform.Translate(Vector3.forward * dirZ * snapSize);
            }

            Debug.Log("DiffX:" + diffX + ", DiffZ:" + diffZ);
        }

        if (other.tag.Contains("Enemy"))
        {
            Vector3 originalPos = other.transform.position;
            Vector3 direction = (player.position - other.transform.position).normalized;
            Vector3 newPos = other.transform.position += direction * 30 + new Vector3 (Random.Range(0, 4), 0, Random.Range(0, 4));

            //Debug.Log("Original Pos:" + originalPos + "New Pos: " + newPos);

        }
    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            Debug.Log("Tile!!!");
        }
    }
}
