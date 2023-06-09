using System;

[Serializable]
public class PlayerData
{
    public int Rating;
    public int LastOpenedQuestId;
    public int Money;
    public int PlayedQuestsCount;

    public PlayerData()
    {
        Rating = 0;
        LastOpenedQuestId = 0;
        Money = 0;
        PlayedQuestsCount = 0;
    }
}
