using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WantedBossManager : MonoBehaviour
{
    [SerializeField] private TMP_Text stageTimerText;
    [SerializeField] private Image RemainImage;
    public GameObject bossPrefab; 
    public Transform spawnPoint; 
    private GameObject currentBoss;
    private bool isBossActive = false;
    private bool isActive = false;
    private float bossCooldown = 60f;
    private float bossDuration = 40f;
    private float cooldownTimer;
    private void Start()
    {
        cooldownTimer = 0f;
        StartCoroutine(BossSpawnRoutine());
    }
    private void Update()
    {
        DuringCoolTimer();
    }
    private void UpdateStageTimerUI(float remainingTime)
    {
        if (stageTimerText != null)
        {
            stageTimerText.text = $"Wanted Remain: {Mathf.FloorToInt(remainingTime)}";
        }
    }
    public void SetBoss(GameObject boss)
    {
        bossPrefab = boss;
    }
    private IEnumerator BossSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossCooldown);
            SpawnBoss();
        }
    }
    public void DuringCoolTimer()
    {
        float elapsedTime = Time.time - cooldownTimer;

        if (elapsedTime < bossCooldown)
        {
            // 남은 시간 갱신
            RemainImage.fillAmount = (bossCooldown - elapsedTime) / bossCooldown;
        }
        else
        {
            // 쿨타임이 끝났다면
            RemainImage.fillAmount = 1f;
            isActive = true;
        }
    }
    public void SpawnBoss()
    {
        if (isBossActive) return;
        if (!isActive) return;

        isActive = false;
        cooldownTimer = Time.time;
        currentBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        isBossActive = true;

        Boss bossComponent = currentBoss.GetComponent<Boss>();
        if (bossComponent != null)
        {
            bossComponent.OnDeath += HandleBossDefeat;
        }
        
        StartCoroutine(BossDurationTimer());
    }

    private IEnumerator BossDurationTimer()
    {
        float timer = bossDuration;

        while (timer > 0)
        {
            UpdateStageTimerUI(timer);
            if (!isBossActive)
                break;
            yield return new WaitForSeconds(0.1f);
            timer -= 0.1f;
        }

        if (isBossActive)
        {
            RemoveBoss(false);
        }

        stageTimerText.text = "";


    }

    private void HandleBossDefeat(GameObject mob, Reward reward)
    {
        GiveReward(currentBoss.GetComponent<Boss>().reward);
        stageTimerText.text = "";
        RemoveBoss(true);
    }

    private void RemoveBoss(bool defeated)
    {
        if (currentBoss != null)
        {
            Destroy(currentBoss);
            currentBoss = null;
        }

        isBossActive = false;

        if (defeated)
        {

        }
        else
        {
            SignManager.Instance.ShowSign($"Wanted is runaway", 2f);
        }
    }

    void GiveReward(Reward reward)
    {
        SignManager.Instance.ShowSign($"Wanted Clear \nGet {reward.rewardType} x {reward.amount}", 2f);

        switch (reward.rewardType)
        {
            case RewardType.Gold:
                GameObject.FindWithTag("Player").GetComponent<Wallet>().Gold += reward.amount;
                break;
            case RewardType.Reforge:
                GameObject.FindWithTag("Player").GetComponent<Wallet>().Reforge += reward.amount;
                break;
        }
    }
}
