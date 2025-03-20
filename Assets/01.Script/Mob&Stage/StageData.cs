using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Stage/StageData")]

[Serializable]
public class StageData : ScriptableObject
{
    public List<MobSpawnInfo> mobSpawnList = new List<MobSpawnInfo>();
    public Reward stageClearReward;
    public float spawnDelay;
    public float stageDuration;
    public bool isBossMap;
}
[Serializable]
public class MobSpawnInfo
{
    public GameObject mobPrefab;
    public int spawnCount;
    public Reward reward;
}