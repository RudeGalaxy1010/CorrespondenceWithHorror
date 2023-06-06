using IJunior.TypedScenes;

public class QuestLoader : IInitable, IDeinitable
{
    private GameData _gameData;
    private PlayerData _playerData;
    private QuestPicker _questPicker;
    private QuestLoaderEmitter _questLoaderEmitter;

    public QuestLoader(GameData gameData, PlayerData playerData, QuestPicker questPicker, 
        QuestLoaderEmitter questLoaderEmitter)
    {
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
        GameScene.Load(
            new QuestLevelData() 
            { 
                GameData = _gameData,
                Quest = _questPicker.CurrentQuest, 
                PlayerData = _playerData 
            });
    }
}
