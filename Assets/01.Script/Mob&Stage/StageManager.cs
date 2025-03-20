using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TMP_Text stageTimerText;
    [SerializeField] private List<StageData> stageList;
    private int stageIndex = 0;
    [SerializeField] private Transform spawnPoint;  
    [SerializeField] private StageData currentStageData; 
    private Queue<MobSpawnInfo> mobSpawnQueue = new Queue<MobSpawnInfo>(); 
    private List<GameObject> activeMobs = new List<GameObject>(); 
    public bool isStageWorking = false;

    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        stageIndex = 0;
        StartStage();
    }
    private void UpdateStageTimerUI(float remainingTime)
    {
        if (stageTimerText != null)
        {
            stageTimerText.text = $"Stage Remain: {Mathf.FloorToInt(remainingTime)}";
        }
    }
    public void StartStage()
    {
        if (stageIndex >= stageList.Count)
        {
            
            return;
        }

        Debug.Log($"스테이지 {stageIndex + 1}");
        PrepareStage();
        StartCoroutine(StageRoutine());
    }

    public void PrepareStage()
    {
        mobSpawnQueue.Clear(); 
        currentStageData = stageList[stageIndex];
        foreach (var mobInfo in currentStageData.mobSpawnList)
        {
            for (int i = 0; i < mobInfo.spawnCount; i++)
            {
                mobSpawnQueue.Enqueue(mobInfo); 
            }
        }
    }

    IEnumerator StageRoutine()
    {
        isStageWorking = true;
        float stageStartTime = Time.time;
        float stageDuration = currentStageData.stageDuration;
        SignManager.Instance.ShowSign($"{stageIndex+1} Stage Start", 2f);
        while (Time.time - stageStartTime <= currentStageData.stageDuration)
        {
            float remainingTime = stageDuration - (Time.time - stageStartTime);
            UpdateStageTimerUI(remainingTime);
            if (mobSpawnQueue.Count > 0)
            {
                SpawnNextMob();
                yield return new WaitForSeconds(currentStageData.spawnDelay);
            }
            else
            {
                yield return null;
            }
        }

        isStageWorking = false;
        UpdateStageTimerUI(0);
        GiveReward(currentStageData.stageClearReward);
        SignManager.Instance.ShowSign($"Stage Clear Get {currentStageData.stageClearReward.rewardType} x {currentStageData.stageClearReward.amount}", 2f);

        NextStage();
    }


    void SpawnNextMob()
    {
        if (mobSpawnQueue.Count == 0) return;

        MobSpawnInfo mobInfo = mobSpawnQueue.Dequeue();
        GameObject mob = Instantiate(mobInfo.mobPrefab, spawnPoint.position, Quaternion.identity);
        mob.name = $"{mobInfo.mobPrefab.name}_{activeMobs.Count + 1}";
        activeMobs.Add(mob);

        Mob mobComponent = mob.GetComponent<Mob>();
        if (mobComponent != null)
        {
            mobComponent.SetReward(mobInfo.reward);
            mobComponent.OnDeath += HandleMobDeath;
        }
    }


    void HandleMobDeath(GameObject mob, Reward reward)
    {
        activeMobs.Remove(mob);
        Destroy(mob);
        GiveReward(reward);
    }

    void GiveReward(Reward reward)
    {
        Debug.Log($"{reward.rewardType} x {reward.amount}");
        switch (reward.rewardType)
        {
            case RewardType.Gold:
                player.GetComponent<Wallet>().Gold += reward.amount;
                break;
            case RewardType.Reforge:
                player.GetComponent<Wallet>().Reforge += reward.amount;
                break;
        }
    }

    void NextStage()
    {
        stageIndex++;

        if (stageIndex < stageList.Count)
        {
            StartStage();
        }
        else
        {
            SignManager.Instance.ShowSign($"Game Clear", 2f);

        }
    }
}