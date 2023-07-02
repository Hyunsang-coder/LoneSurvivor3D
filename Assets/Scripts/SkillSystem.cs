using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public List<Skill> skillList;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[System.Serializable]
public class Skill
{
    public string skillName;
    public float coolTime;
    public Sprite icon;


}
