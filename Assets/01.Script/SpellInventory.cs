using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    public TMP_Text spellNum;
    public Dictionary<Spell, int> spellInventory = new Dictionary<Spell, int>();

    public void AddSpell(SpellData spell)
    {
        Spell existingSpell = FindExistingSpell(spell);
        if (existingSpell != null) 
        {
            spellInventory[existingSpell]++;  
            existingSpell.count= spellInventory[existingSpell];
        }
        else
        {
            GameObject s = new GameObject($"{spell.spellSort}_{spell.rarity}");
            s.AddComponent<Spell>();
            s.GetComponent<Spell>().OnInitialized(spell);
            spellInventory[s.GetComponent<Spell>()] = 1;  

        }
        spellNum.text = GetTotalSpellCount().ToString();
    }
    public int GetSpellCount(SpellData spell)
    {
        Spell s =FindExistingSpell(spell);
        return spellInventory.GetValueOrDefault(s, 0);
    }

    public int GetTotalSpellCount()
    {
        return spellInventory.Values.Sum();
    }
    public Spell FindExistingSpell(SpellData newSpell)
    {
        return spellInventory.Keys.FirstOrDefault(spell =>
            spell.Data.rarity == newSpell.rarity && spell.Data.spellSort == newSpell.spellSort);
    }

    public void AddSpell(SpellSort spellSort, int rarity)
    {
        SpellData spell = LoadSpellFromResources(spellSort, rarity);
        if (spell != null)
        {
            if (rarity >= 4)
            {
                SignManager.Instance.ShowSign($"Get {rarity} Rarity Spell", 2f);
            }
            AddSpell(spell);
        }
        else
        {
            Debug.LogWarning($"{spellSort} {rarity}성 폴더에 없음");
        }
        spellNum.text = GetTotalSpellCount().ToString();
    }

    public SpellData LoadSpellFromResources(SpellSort spellSort, int rarity)
    {
        SpellData[] spells = Resources.LoadAll<SpellData>("Spell"); 

        foreach (SpellData spell in spells)
        {
            if (spell.spellSort == spellSort && spell.rarity == rarity)
            {
                return spell;
            }
        }

        return null; 
    }
}