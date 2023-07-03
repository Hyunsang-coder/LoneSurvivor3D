using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public HealthSystem playerHealth;

    public Slider HPslider;
    public Slider XPslider;

    public Text level;
    public Text kill;
    public Text stage;
    public GameObject skillUI;
    public Transform template;

    public float spaceBtwSkills = 120f;

    private void Awake() {
        HPslider = transform.Find("HealthUI").GetComponent<Slider>();
        XPslider = transform.Find("ExpUI").GetComponent<Slider>();
        level = transform.Find("Level").GetComponent<Text>();
        kill = transform.Find("Kill").GetComponent<Text>();
        stage = transform.Find("Stage").GetComponent<Text>();
        skillUI = transform.Find("SkillUI").gameObject;

        template = skillUI.transform.Find("Template");
        template.gameObject.SetActive(false);
    }
    
    void Start()
    {
        gameManager =  GameManager.Instance;

        gameManager.onKillScoreChange += UpdateUI;
        playerHealth.onTakeDamage += UpdateUI;

        UpdateUI();
        UpdateSkillUI();
        
        StartCoolTime("Test1", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        HPslider.value = playerHealth.health / playerHealth.MaxHealth;
        XPslider.value = (float) gameManager.currentKillCount / gameManager.levelUpThreshold;
        level.text = "Level: " + gameManager.playerLevel.ToString();
        kill.text = "Kill: " + gameManager.totalKillCount.ToString();
        stage.text = "Stage: " + gameManager.gameStage.ToString();

    }

    public void UpdateSkillUI()
    {
        int skillCount = gameManager.player.skillSystem.skillList.Count;
        Debug.Log("Skill count: " + skillCount);

        for (int i = 0; i < skillCount; i++)
        {
            Transform skillTransform = Instantiate(template, skillUI.transform);
            skillTransform.gameObject.name = gameManager.player.skillSystem.skillList[i].skillName;
            
            skillTransform.gameObject.SetActive(true);
            
            float x = (skillCount % 2 == 1) ? spaceBtwSkills * (i - (skillCount - 1) / 2) : spaceBtwSkills * (i - skillCount / 2 + 0.5f);
            skillTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2 (x, 0);
            skillTransform.GetComponent<Image>().sprite = gameManager.player.skillSystem.skillList[i].icon;

            Image cover = skillTransform.Find("Cover").GetComponent<Image>();
            cover.fillAmount = 0;
        }

        Debug.Log("Skill update");
    }

    public void StartCoolTime(string skill, float coolTime)
    {
        Transform skillTransform = skillUI.transform.Find(skill);
        
        
        if (skillTransform) 
        {
            Transform cover = skillTransform.Find("Cover");
            StartCoroutine(CoolTimeCoroutine(cover, coolTime));
        }
        
        Debug.Log(skill + "is null!!");
    }

    IEnumerator CoolTimeCoroutine(Transform cover, float duration)
    {
        float currentTime = Time.time; 
        cover.GetComponent<Image>().fillAmount = 1;
        while (currentTime - Time.time < duration)
        {
            cover.GetComponent<Image>().fillAmount -= Time.deltaTime/duration;
            yield return null;
        }
        
    }

    
}
