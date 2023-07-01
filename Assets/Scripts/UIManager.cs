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
    private void Awake() {
        HPslider = transform.Find("HealthUI").GetComponent<Slider>();
        XPslider = transform.Find("ExpUI").GetComponent<Slider>();
        level = transform.Find("Level").GetComponent<Text>();
        kill = transform.Find("Kill").GetComponent<Text>();
        stage = transform.Find("Stage").GetComponent<Text>();
    }
    
    void Start()
    {
        gameManager =  GameManager.Instance;

        gameManager.onKillScoreChange += UpdateUI;
        playerHealth.onTakeDamage += UpdateUI;

        UpdateUI();
        
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
}
