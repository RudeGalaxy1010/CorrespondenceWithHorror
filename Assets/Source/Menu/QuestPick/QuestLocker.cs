public class QuestLocker : IInitable, IDeinitable
{
    private PlayerData _playerData;
    private QuestPicker _questPicker;
    private QuestLoaderEmitter _emitter;

    public QuestLocker(PlayerData playerData, QuestPicker questPicker, QuestLoaderEmitter emitter)
    {
        _playerData = playerData;
        _questPicker = questPicker;
        _emitter = emitter;
    }

    public void Init()
    {
        _questPicker.QuestUpdated += OnQuestUpdated;
    }

    public void Deinit()
    {
        _questPicker.QuestUpdated -= OnQuestUpdated;
    }

    private void OnQuestUpdated(Quest quest)
    {
        bool isQuestOpened = quest.Id <= _playerData.LastOpenedQuestId;
        _emitter.LoadButton.interactable = isQuestOpened;
    }
}
