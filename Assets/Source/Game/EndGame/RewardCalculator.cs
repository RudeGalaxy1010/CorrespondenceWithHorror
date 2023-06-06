using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCalculator
{
    private const int VictoryRewardUnder5thQuest = 200;
    private const int VictoryRewardUnder10thQuest = 300;
    private const int VictoryRewardUnder20thQuest = 350;
    private const int VictoryRewardOver20thQuest = 500;
    private const int DefeatReward = 50;

    private Quest _quest;

    public RewardCalculator(Quest quest)
    {
        _quest = quest;
    }

    public int GetReward(GameResult result)
    {
        if (result == GameResult.Victory)
        {
            return GetVictoryResult();
        }
        else
        {
            return DefeatReward;
        }
    }

    private int GetVictoryResult()
    {
        switch (_quest.Id)
        {
            case > 20: return VictoryRewardOver20thQuest;
            case > 10: return VictoryRewardUnder20thQuest;
            case > 5: return VictoryRewardUnder10thQuest;
            default: return VictoryRewardUnder5thQuest;
        }
    }
}
