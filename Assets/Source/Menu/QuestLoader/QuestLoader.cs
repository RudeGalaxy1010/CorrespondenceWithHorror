using IJunior.TypedScenes;

public class QuestLoader : IInitable, IDeinitable
{
    private PlayerData _playerData;
    private QuestPicker _questPicker;
    private QuestLoaderEmitter _questLoaderEmitter;

    public QuestLoader(PlayerData playerData, QuestPicker questPicker, QuestLoaderEmitter questLoaderEmitter)
    {
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
        GameScene.Load(new QuestData() { Quest = _questPicker.CurrentQuest, PlayerData = _playerData });
    }
}
