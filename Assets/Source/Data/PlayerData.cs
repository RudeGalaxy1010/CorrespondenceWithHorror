using System;

[Serializable]
public class PlayerData
{
    public int Rating;
    public int LastOpenedQuestId;
    public int Money;
    public int PlayedQuestsCount;
    public int CurrentAvatarId;
    public int[] OpenedAvatarIds;

    public PlayerData()
    {
        Rating = 0;
        LastOpenedQuestId = 0;
        Money = 0;
        PlayedQuestsCount = 0;
        CurrentAvatarId = 0;
        OpenedAvatarIds = new int[] { 0 };
    }
}
