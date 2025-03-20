using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellBTN : MonoBehaviour
{

    public TMP_Text text;
    public int count;
    public SpellInventory inventory;
    public SpellData spell;
    public Action OnUse;

    private float cooldownTime;
    private float cooldownTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnUse += UsedSpell;
        gameObject.GetComponent<Button>().onClick.AddListener(OnMergeSpell);
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.FindExistingSpell(spell) != null) {
            if (!inventory.spellInventory.ContainsKey(inventory.FindExistingSpell(spell)))
                return;
            if(inventory.FindExistingSpell(spell).spellBTN == null)
                inventory.FindExistingSpell(spell).spellBTN = this;
            count = inventory.spellInventory[inventory.FindExistingSpell(spell)];
            DuringCoolTimer();
            text.text = count.ToString();
        }
    }
    public void DuringCoolTimer()
    {
        cooldownTimer -= Time.deltaTime;
        gameObject.GetComponent<Image>().fillAmount = cooldownTimer / cooldownTime;
        if (cooldownTimer <= 0)
        {
            gameObject.GetComponent<Image>().fillAmount = 1; 
        }
    }
    private void UpdateCooldownTime()
    {
        int spellCount = GameObject.FindWithTag("Player").GetComponent<SpellInventory>().GetSpellCount(spell);
        if(cooldownTime!= spell.cooldown / Mathf.Max(1, spellCount))
            cooldownTime = spell.cooldown / Mathf.Max(1, spellCount); 
    }

    public void UsedSpell()
    {
            UpdateCooldownTime();
            cooldownTimer = cooldownTime;
    }
    public void OnMergeSpell()
    {
        if (count < 3)
            return;
        
        if (inventory.GetSpellCount(spell) >= 3)
        {
            inventory.spellInventory[inventory.FindExistingSpell(spell)] = inventory.spellInventory[inventory.FindExistingSpell(spell)] - 3;
            inventory.FindExistingSpell(spell).count = inventory.FindExistingSpell(spell).count - 3;
            SpellSort s_sort = (SpellSort)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(SpellSort)).Length);
            inventory.AddSpell(s_sort, spell.rarity+1);

        }
    }
}
