using UnityEngine;
using UnityEngine.UI;

public class Boss : Mob
{
    public Scrollbar hpBar;
    public Vector3 HpBarOffset;

    private float MAX_Health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            hpBar.size = Health / MAX_Health;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Start()
    {
        base.Start();
        MAX_Health = health;
    }
   
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        HpBarMove();
    }
    public override void TakeDamage(float damage)
    {
        Health = Health - damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void HpBarMove()
    {
        Vector3 worldPosition = transform.position + HpBarOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        hpBar.transform.position = screenPosition;
    }
}
