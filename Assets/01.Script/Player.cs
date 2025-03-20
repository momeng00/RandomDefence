using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class ReforgeLevel
{
    public SpellSort spellSort;
    public int level;
}

public class Player : MonoBehaviour
{
    public Scrollbar hpBar;
    public Vector3 HpBarOffset;
    public float health;
    public List<ReforgeLevel> reforgeLevels = new List<ReforgeLevel>();
    private float MAX_Health;
    public float Health
    {
        get { return health; }
        set 
        { 
            health = value;
            hpBar.size = Health / MAX_Health;
            if (health <= 0)
            {
                SignManager.Instance.ShowSign("GameOver", 3f);
                Invoke("ChangeScene", 3f);
            }
        }
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("LobbyScenes");
    }
    public ReforgeLevel GetReforgeLevel(SpellSort sort)
    {
        return reforgeLevels.Find(r => r.spellSort == sort);
    }
    private void Start()
    {
        MAX_Health = health;
        HpBarMove();
    }
    private void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    public void HpBarMove()
    {
        hpBar.transform.position = gameObject.transform.position + HpBarOffset;
    }
}