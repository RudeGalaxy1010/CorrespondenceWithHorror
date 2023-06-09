using IJunior.TypedScenes;
using System;
using System.Linq;

public class SceneLoader
{
    private const string QuestLevelDataNotSetExceptionMessage = "Quest level data has to be set first!";

    private QuestLevelData _questLevelData;

    public SceneLoader(QuestLevelData questLevelData = null)
    {
        _questLevelData = questLevelData;
    }

    public void LoadMenu()
    {
        MenuScene.Load();
    }

    public void LoadQuest(QuestLevelData questLevelData)
    {
        GameScene.Load(questLevelData);
    }

    public void LoadNextQuest()
    {
        if (_questLevelData == null)
        {
            throw new Exception(QuestLevelDataNotSetExceptionMessage);
        }

        _questLevelData.Quest = GetNextQuest();
        LoadQuest(_questLevelData);
    }

    public void ReloadQuest()
    {
        if (_questLevelData == null)
        {
            throw new Exception(QuestLevelDataNotSetExceptionMessage);
        }

        LoadQuest(_questLevelData);
    }

    private Quest GetNextQuest()
    {
        GameData gameData = _questLevelData.GameData;
        int currentQuestId = _questLevelData.Quest.Id;
        int lastQuestId = gameData.Quests.Max(q => q.Id);

        if (currentQuestId == lastQuestId)
        {
            int firstQuestId = gameData.Quests.Min(q => q.Id);
            return gameData.Quests.First(q => q.Id == firstQuestId);
        }

        return gameData.Quests.First(q => q.Id > currentQuestId);
    }
}
