using UnityEngine;

public class QuestLoader : IInitable, IDeinitable
{
    private SceneLoader _sceneLoader;
    private GameData _gameData;
    private PlayerData _playerData;
    private QuestPicker _questPicker;
    private AvatarDisplayer _avatarDisplayer;
    private Sprite[] _heroAvatas;
    private QuestLoaderEmitter _questLoaderEmitter;

    public QuestLoader(SceneLoader sceneLoader, GameData gameData, PlayerData playerData, QuestPicker questPicker,
        AvatarDisplayer avatarDisplayer, Sprite[] heroAvatas, QuestLoaderEmitter questLoaderEmitter)
    {
        _sceneLoader = sceneLoader;
        _gameData = gameData;
        _playerData = playerData;
        _questPicker = questPicker;
        _avatarDisplayer = avatarDisplayer;
        _heroAvatas = heroAvatas;
        _questLoaderEmitter = questLoaderEmitter;
    }

    public void Init()
    {
        _questLoaderEmitter.LoadButton.onClick.AddListener(OnLoadButtonClicked);
    }

    public void Deinit()
    {
        _questLoaderEmitter.LoadButton.onClick.RemoveListener(OnLoadButtonClicked);
    }

    private void OnLoadButtonClicked()
    {
        var levelData = new LevelData()
        {
            GameData = _gameData,
            Quest = _questPicker.CurrentQuest,
            PlayerData = _playerData,
            PlayerAvatar = _avatarDisplayer.CurrentAvatar,
            HeroAvatars = _heroAvatas
        };
        _sceneLoader.LoadQuest(levelData);
    }
}
