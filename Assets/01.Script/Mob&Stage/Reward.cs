
using System;

public enum RewardType
{
    Gold,
    Reforge,

}
[Serializable]
public class Reward
{
    public RewardType rewardType;
    public int amount;
}