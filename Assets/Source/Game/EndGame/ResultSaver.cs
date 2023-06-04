public class ResultSaver : IInitable, IDeinitable
{
    private const int QuestsForLevelUp = 3;

    private EndGame _endGame;
    private PlayerData _playerData;
    private SaveLoad _saveLoad;
    private Quest _quest;

    public ResultSaver(PlayerData playerData, Quest quest, EndGame endGame, SaveLoad saveLoad)
    {
        _playerData = playerData;
        _quest = quest;
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

    public bool NeedUpdateLevel => _playerData.PlayedQuestsCount % QuestsForLevelUp == 0;

    private void OnGameEnded(GameResult result)
    {
        if (result != GameResult.Victory)
        {
            return;
        }

        _playerData.LastOpenedQuestId = _quest.Id;
        _playerData.PlayedQuestsCount++;

        if (NeedUpdateLevel == true)
        {
            _playerData.Level++;
        }

        _saveLoad.SavePlayerData(_playerData);
    }
}
