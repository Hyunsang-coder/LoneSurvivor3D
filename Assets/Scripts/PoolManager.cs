using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public static PoolManager Instance;
    public GameObject[] prefabArray;
    List<GameObject>[] prefabArrayList;

    

    private void Awake()
    {
        // 배열과 리스트 초기화 
        prefabArrayList = new List<GameObject>[prefabArray.Length];

        for (int i = 0; i < prefabArrayList.Length; i++)
        {
            prefabArrayList[i] = new List<GameObject>();
        }

        Instance = this;
    }
   

    // Update is called once per frame
    void Update()
    {   
        
    }



    public GameObject GetObject(int index)
    {
        GameObject selected = null;

        // select an object that is not used

        // if there is one, assign it to selected

        foreach (GameObject item in prefabArrayList[index])
        {
            if (!item.activeSelf) {
                selected = item;
                selected.SetActive(true);
                break;
            }
        }

        // if not, create a new one and assign it to selected 
        if (!selected) {
            selected = Instantiate(prefabArray[index], transform);
        }

        //pool에 등록!
        prefabArrayList[index].Add(selected);

        return selected;
    }
}
