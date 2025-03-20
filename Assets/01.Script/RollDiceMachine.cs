using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class FloatArray
{
    public float[] values;
}
public class RollDiceMachine : MonoBehaviour
{
    
    public SpellInventory inventory;
    public Wallet wallet;
    public List<FloatArray> rarity = new List<FloatArray>();
    private int rarityIndex = 0;
    private void Update()
    { 
    }

    private void Start()
    {
        rarityIndex = 0;
    }
    public int RollDice()
    {
        if (!wallet.UseGold())
            return 0;
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        float cumulative = 0;

        for(int i = 0; i < rarity[rarityIndex].values.Length; i++)
        {
            cumulative += rarity[rarityIndex].values[i];
            if (randomValue < cumulative)
            {
                return i+1;
            }
        }

        return 0;
    }

    public void ReturnItem()
    {
        if(inventory.GetTotalSpellCount()>=25){
            SignManager.Instance.ShowSign($"Limit maximum number of Spell", 2f);
            return;
        }
        int rarity = RollDice();
        if (rarity <= 0)
            return;
        SpellSort spell = (SpellSort)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(SpellSort)).Length);
        inventory.AddSpell(spell, rarity);
        
    }

    public void UpgradeRandom()
    {
        if (rarity[rarityIndex+1] != null)
            rarityIndex++;
        return;
    }
}