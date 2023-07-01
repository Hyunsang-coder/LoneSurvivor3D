using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;

    public static GameManager Instance;

    public Action onKillScoreChange;

    public int totalKillCount;
    public int currentKillCount;
    public float gameTime;

    public int gameStage;

    public int levelUpThreshold;

    public int playerLevel;

    private void Awake() {
        Instance = this;

        totalKillCount = 0;
        currentKillCount = 0;
    }
    void Start()
    {
        UpdateLevelUpThreshold(player.level);
        playerLevel = player.level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreKill()
    {
        totalKillCount ++;
        currentKillCount++;
        
        if (totalKillCount == levelUpThreshold)
        {
            LevelUp();
        }

        onKillScoreChange?.Invoke();
    }

    void LevelUp()
    {
        UpdateLevelUpThreshold(player.PlayerLevelUp());
        currentKillCount = 0;
        playerLevel = player.level;
    }

    public void UpdateLevelUpThreshold(int playerLevel)
    {
        levelUpThreshold = playerLevel* 10;
    }
}
