using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Player player;
    private ProjectileData _data;
    public SpellData spellData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(0f, -2.5f, 0f),0.01f*spellData.moveSpeed);
        Del();
    }
    public void Del()
    {
        if (transform.position.y < -2.46f)
            ResourceManager.Instance.Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mob mob = collision.GetComponent<Mob>();
        if (mob!=null)
        {
            Debug.Log(spellData.damage * (player.GetReforgeLevel(spellData.spellSort).level+1));
            mob.TakeDamage(spellData.damage * (player.GetReforgeLevel(spellData.spellSort).level+1));
            ResourceManager.Instance.Destroy(gameObject);
        }
    }
}
