public class QuestLoader : IInitable, IDeinitable
{
    private SceneLoader _sceneLoader;
    private GameData _gameData;
    private PlayerData _playerData;
    private QuestPicker _questPicker;
    private QuestLoaderEmitter _questLoaderEmitter;

    public QuestLoader(SceneLoader sceneLoader, GameData gameData, PlayerData playerData, QuestPicker questPicker,
        QuestLoaderEmitter questLoaderEmitter)
    {
        _sceneLoader = sceneLoader;
        _gameData = gameData;
        _playerData = playerData;
        _questPicker = questPicker;
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
        var questLevelData = new QuestLevelData()
        {
            GameData = _gameData,
            Quest = _questPicker.CurrentQuest,
            PlayerData = _playerData
        };
        _sceneLoader.LoadQuest(questLevelData);
    }
}
