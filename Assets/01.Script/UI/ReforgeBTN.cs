using UnityEngine;

public class ReforgeBTN : UIBTN
{
    public SpellSort spellSort;
    public ReforgeManager reforgeManager;

    public void UpgradeSpell()
    {
        reforgeManager.Upgrade(spellSort);
    }
}

