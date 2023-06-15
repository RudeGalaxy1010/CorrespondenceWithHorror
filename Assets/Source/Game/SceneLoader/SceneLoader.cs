using IJunior.TypedScenes;
using System;
using System.Linq;

public class SceneLoader
{
    private const string QuestLevelDataNotSetExceptionMessage = "Quest level data has to be set first!";

    private LevelData _levelData;

    public SceneLoader(LevelData questLevelData = null)
    {
        _levelData = questLevelData;
    }

    public void LoadMenu()
    {
        MenuScene.Load();
    }

    public void LoadQuest(LevelData questLevelData)
    {
        GameScene.Load(questLevelData);
    }

    public void LoadNextQuest()
    {
        if (_levelData == null)
        {
            throw new Exception(QuestLevelDataNotSetExceptionMessage);
        }

        _levelData.Quest = GetNextQuest();
        LoadQuest(_levelData);
    }

    public void ReloadQuest()
    {
        if (_levelData == null)
        {
            throw new Exception(QuestLevelDataNotSetExceptionMessage);
        }

        LoadQuest(_levelData);
    }

    private Quest GetNextQuest()
    {
        GameData gameData = _levelData.GameData;
        int currentQuestId = _levelData.Quest.Id;
        int lastQuestId = gameData.Quests.Max(q => q.Id);

        if (currentQuestId == lastQuestId)
        {
            int firstQuestId = gameData.Quests.Min(q => q.Id);
            return gameData.Quests.First(q => q.Id == firstQuestId);
        }

        return gameData.Quests.First(q => q.Id > currentQuestId);
    }
}
