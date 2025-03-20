using System;
using System.Collections.Generic;
using UnityEngine;


public class Mob : MonoBehaviour
{
    public float moveSpeed;
    public Reward reward;
    public float health;
    public event Action<GameObject, Reward> OnDeath;
    public Player target;
    public float damage;
    public float attackDelay;

    private bool canAttack=false;
    private float attackTimeMark;
    public Dictionary<DebuffType, Action> activeDebuff = new Dictionary<DebuffType, Action>();

    public virtual void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackTimeMark = 0f;
    }
    public virtual void Update()
    {
        if (canAttack)
        {
            if (Time.time >= attackTimeMark + attackDelay)
            {
                attackTimeMark = Time.time;
                target.TakeDamage(damage);
            }
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position + Vector3.down*0.5f, 0.01f * moveSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            canAttack = true;
        }
    }


    public virtual void TakeDamage(float damage)
    {
        health = health-damage;
        if (health<=0) 
        {
            Die();
        }
    }
    public void SetReward(Reward rewardData)
    {
        reward = rewardData;
    }
    public void Die()
    {
        OnDeath?.Invoke(gameObject, reward);
    }
    public void OnDebuff(DebuffType type, Action act)
    {
        if (!activeDebuff.ContainsKey(type)) 
        { 
            activeDebuff.Add(type, act);
            activeDebuff[type]?.Invoke();
        }
    }
    public void OffDebuff(DebuffType type)
    {
        if (!activeDebuff.ContainsKey(type))
        {
            activeDebuff.Remove(type);
        }
    }
}