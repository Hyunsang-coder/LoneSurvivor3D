using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public GameObject[] props;
    public List<GameObject> instantiatedProps;

    float posX;
    float posZ;

    public int propNumber;


    private void Awake() {
       
    }
    void Start()
    {
       PlaceProps();

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceProps()
    {
        if (props == null)
        {
            Debug.Log("assign prop prefabs!");
            return;
        }

        foreach (GameObject prop in props)
       {
            GameObject instance = Instantiate(prop);
            instantiatedProps.Add(instance);
            instance.transform.parent = transform;
            instance.SetActive(false);
       }

       ReplaceProps(); 
        
    }

    public void ReplaceProps()
    {
        foreach (GameObject obj in instantiatedProps)
        {
            obj.SetActive(false);
        }

        for(int i = 0; i < propNumber;  i++)
        {
            float posX = Random.Range(1f, 4f);
            float posZ = Random.Range(1f, 4f);
            float offSetX = Random.Range(-0.5f, 0.5f);
            float offSetZ = Random.Range(-0.5f, 0.5f);

            int randomIndex = Random.Range(0, props.Length);

            instantiatedProps[randomIndex].SetActive(true);


           switch(i){
                case 1:
                    instantiatedProps[randomIndex].transform.localPosition = new Vector3(-(posX + offSetX), 0, (posZ + offSetZ));
                    break;
                case 2:
                    instantiatedProps[randomIndex].transform.localPosition = new Vector3(-(posX + offSetX), 0, -(posZ + offSetZ));
                    break;
                case 3:
                    instantiatedProps[randomIndex].transform.localPosition = new Vector3((posX + offSetX), 0, -(posZ + offSetZ));
                    break;
                default:
                    instantiatedProps[randomIndex].transform.localPosition = new Vector3((posX + offSetX), 0, (posZ + offSetZ));
                    break;
            }
        }
    }

}
