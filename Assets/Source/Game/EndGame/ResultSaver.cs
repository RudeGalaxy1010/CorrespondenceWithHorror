using System;
using System.Linq;

public class ResultSaver : IInitable, IDeinitable
{
    private const int QuestsForLevelUp = 3;

    private QuestLevelData _questLevelData;
    private EndGame _endGame;
    private SaveLoad _saveLoad;

    public ResultSaver(QuestLevelData questLevelData, EndGame endGame, SaveLoad saveLoad)
    {
        _questLevelData = questLevelData;   
        _endGame = endGame;
        _saveLoad = saveLoad;
    }

    public void Init()
    {
        _endGame.GameEnded += OnGameEnded;
    }

    public void Deinit()
    {
        _endGame.GameEnded -= OnGameEnded;
    }

    public bool NeedUpdateLevel => _questLevelData.PlayerData.PlayedQuestsCount % QuestsForLevelUp == 0;

    private void OnGameEnded(GameResult result)
    {
        if (result != GameResult.Victory)
        {
            return;
        }

        PlayerData playerData = _questLevelData.PlayerData;
        playerData.LastOpenedQuestId = GetNextQuestId(_questLevelData.Quest);
        playerData.PlayedQuestsCount++;

        if (NeedUpdateLevel == true)
        {
            playerData.Rating++;
        }

        _saveLoad.SavePlayerData(playerData);
    }

    private int GetNextQuestId(Quest quest)
    {
        Quest nextQuest = _questLevelData.GameData.Quests.FirstOrDefault(q => q.Id > quest.Id);
        return nextQuest != null ? nextQuest.Id : quest.Id;
    }
}
