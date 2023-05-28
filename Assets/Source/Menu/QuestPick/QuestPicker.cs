public class QuestPicker : IInitable, IDeinitable
{
    private const string DefaultQuestName = "-";

    private GameData _gameData;
    private QuestPickerEmitter _emitter;
    private int _currentQuestPointer;

    public QuestPicker(GameData gameData, QuestPickerEmitter emitter)
    {
        _gameData = gameData;
        _emitter = emitter;
        _currentQuestPointer = 0;
        UpdateQuestName();
    }

    public string CurrentQuestName => _gameData.QuestsNames != null || _gameData.QuestsNames.Length > 0 ?
        _gameData.QuestsNames[_currentQuestPointer] : DefaultQuestName;

    public void Init()
    {
        _emitter.NextQuestButton.onClick.AddListener(OnNextQuestButtonClicked);
        _emitter.PreviousQuestButton.onClick.AddListener(OnPreviousQuestButtonClicked);
    }

    public void Deinit()
    {
        _emitter.NextQuestButton.onClick.RemoveListener(OnNextQuestButtonClicked);
        _emitter.PreviousQuestButton.onClick.RemoveListener(OnPreviousQuestButtonClicked);
    }

    private void OnNextQuestButtonClicked()
    {
        _currentQuestPointer++;

        if (_currentQuestPointer >= _gameData.QuestsNames.Length)
        {
            _currentQuestPointer = 0;
        }

        UpdateQuestName();
    }

    private void OnPreviousQuestButtonClicked()
    {
        _currentQuestPointer--;

        if (_currentQuestPointer < 0)
        {
            _currentQuestPointer = _gameData.QuestsNames.Length - 1;
        }

        UpdateQuestName();
    }

    private void UpdateQuestName()
    {
        _emitter.QuestNameText.text = _gameData.QuestsNames.Length > 0 ?
            _gameData.QuestsNames[_currentQuestPointer] : DefaultQuestName;
    }
}
