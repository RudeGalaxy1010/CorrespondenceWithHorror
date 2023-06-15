using System;
using System.Linq;

public class ResultSaver : IInitable, IDeinitable
{
    private const int QuestsForLevelUp = 3;

    private QuestLevelData _questLevelData;
    private SaveLoad _saveLoad;
    private EndGame _endGame;
    private EndGamePanel _endGamePanel;

    private int _playedQuestsCount;
    private int _lastOpenedQuestId;
    private int _rating;
    private int _reward;

    public ResultSaver(QuestLevelData questLevelData, SaveLoad saveLoad, EndGame endGame, EndGamePanel endGamePanel)
    {
        _questLevelData = questLevelData;
        _saveLoad = saveLoad;
        _endGame = endGame;
        _endGamePanel = endGamePanel;
    }

    public void Init()
    {
        _endGame.GameEnded += OnGameEnded;
        _endGamePanel.RewardChanged += OnRewardChanged;
        _endGamePanel.NeedSave += OnNeedSave;
    }

    public void Deinit()
    {
        _endGame.GameEnded -= OnGameEnded;
        _endGamePanel.RewardChanged -= OnRewardChanged;
        _endGamePanel.NeedSave -= OnNeedSave;
    }

    public bool NeedUpdateLevel => _questLevelData.PlayerData.PlayedQuestsCount % QuestsForLevelUp == 0;

    private void OnGameEnded(GameResult result)
    {
        PlayerData playerData = _questLevelData.PlayerData;
        _playedQuestsCount = playerData.PlayedQuestsCount + 1;

        if (result != GameResult.Victory)
        {
            return;
        }

        _lastOpenedQuestId = GetNextQuestId(_questLevelData.Quest);

        if (NeedUpdateLevel == true)
        {
            _rating = playerData.Rating + 1;
        }
    }

    private int GetNextQuestId(Quest quest)
    {
        Quest nextQuest = _questLevelData.GameData.Quests.FirstOrDefault(q => q.Id > quest.Id);
        return nextQuest != null ? nextQuest.Id : quest.Id;
    }

    private void OnRewardChanged(int reward)
    {
        _reward = reward;
    }

    private void OnNeedSave()
    {
        PlayerData playerData = _questLevelData.PlayerData;
        playerData.PlayedQuestsCount = _playedQuestsCount;
        playerData.CurrentAvatarId = _lastOpenedQuestId;
        playerData.Rating = _rating;
        playerData.Money += _reward;
        _saveLoad.SavePlayerData(playerData);
    }
}
