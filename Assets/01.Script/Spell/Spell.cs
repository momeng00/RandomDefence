using UnityEngine;

public class Spell : MonoBehaviour
{
    private SpellData _data;
    private float _cooldown;
    public int count;
    private float timeMark;
    private Player player;
    public SpellBTN spellBTN;
    public SpellData Data { get => _data; }

    public Spell(SpellData data)
    {
        OnInitialized(data);
    }

    private void Update()
    {
        if (Time.time >= timeMark + _cooldown/count)
        {
            timeMark = Time.time;
            UseSpell();
        }
    }

    public void OnInitialized(SpellData data)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _data = data;
        count = 1;
        _cooldown = data.cooldown;
        timeMark = 0f;
    }
    
    public void UseSpell()
    {
        if (count <= 0)
            return;
        _data.OnFireProjectile(player.transform);
        spellBTN.OnUse?.Invoke();
    }
}