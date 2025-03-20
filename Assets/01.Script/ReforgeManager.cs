using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReforgeManager : MonoBehaviour
{
    private Dictionary<SpellSort, int> reforgeLevels = new Dictionary<SpellSort, int>();
    private Player player;
    private Wallet wallet;

    public TMP_Text fireText, waterText, rockText;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        wallet = player.GetComponent<Wallet>();

        foreach (SpellSort type in System.Enum.GetValues(typeof(SpellSort)))
        {
            reforgeLevels[type] = 0;
        }
        UpdateUI();
    }

    public void Upgrade(SpellSort type)
    {
        int currentLevel = reforgeLevels[type];
        int cost = currentLevel + 1;

        if (wallet.Reforge >= cost)
        {
            wallet.Reforge -= cost; 
            reforgeLevels[type]++;
            player.GetReforgeLevel(type).level++;
            UpdateUI();
        }
        else
        {

        }
    }

    private void UpdateUI()
    {
        fireText.text = $"Need {player.GetReforgeLevel(SpellSort.Fire).level+1}R";
        waterText.text = $"Need {player.GetReforgeLevel(SpellSort.Water).level + 1}R";
        rockText.text = $"Need {player.GetReforgeLevel(SpellSort.Rock).level + 1}R";
    }
}