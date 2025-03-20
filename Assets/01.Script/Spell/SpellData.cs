using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/SpellData")]
public class SpellData : ScriptableObject 
{ 
    public GameObject projectile_prefab;
    public SpellSort spellSort;
    public float cooldown;
    public int rarity;
    public float damage;
    public float moveSpeed;

    public void OnFireProjectile(Transform transform)
    {
        GameObject g = ResourceManager.Instance.Spawn(projectile_prefab, transform);
        g.GetComponent<Projectile>().spellData = this;
        g.name = ($"{spellSort}_{rarity}_Projectile");
    }

}
