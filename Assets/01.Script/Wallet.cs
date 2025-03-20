using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text reforgeText;
    public TMP_Text needGold;
    public int goldAmount;
    public int reforgeAmount;
    public int count;
    public int Count
    {
        get { return count; }
        set 
        { 
            count = value;
            needGold.text = ($"{count}G To Create");
        }
    }
    public int Gold
    {
        get { return  goldAmount; }
        set 
        { 
            goldAmount = value;
            goldText.text = goldAmount.ToString(); 
            
        }
    }
    public int Reforge
    {
        get { return reforgeAmount; }
        set
        {
            reforgeAmount = value;
            reforgeText.text = reforgeAmount.ToString();

        }
    }
    private void Start()
    {
        Gold = goldAmount;
        Reforge = reforgeAmount;
        Count = 20;
    }

    public bool UseGold()
    {
        if (Gold < Count)
            return false;
        Gold -= Count;
        Count++;
        return true;
    }
    public bool UseReforge(int value)
    {
        if (Reforge < value)
            return false;
        Reforge -= value;
        return true;
    }
}