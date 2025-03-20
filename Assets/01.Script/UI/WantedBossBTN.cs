using UnityEngine;

public class WantedBossBTN : UIBTN
{
    public GameObject BossPrefab;
    public WantedBossManager wantedBossManager;
    public void SetBossPrefab()
    {
        Debug.Log(gameObject);
        wantedBossManager.SetBoss(BossPrefab);
        wantedBossManager.SpawnBoss();
    }
}

