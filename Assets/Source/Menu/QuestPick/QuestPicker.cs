using System;
using System.Linq;
using UnityEngine;

public class QuestPicker : IInitable, IDeinitable
{
    private const string DefaultQuestName = "-";

    public event Action<Quest> QuestUpdated;

    private GameData _gameData;
    private Sprite[] _questsPreview;
    private QuestPickerEmitter _emitter;
    private int _currentQuestPointer;

    public QuestPicker(GameData gameData, Sprite[] questsPreview, QuestPickerEmitter emitter)
    {
        _gameData = gameData;
        _questsPreview = questsPreview;
        _emitter = emitter;
        _currentQuestPointer = 0;
        UpdateQuestName();
        UpdateAvatar();
    }

    public Quest CurrentQuest => _gameData.Quests != null || _gameData.Quests.Length > 0 ?
        _gameData.Quests[_currentQuestPointer] : null;

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

        if (_currentQuestPointer >= _gameData.Quests.Length)
        {
            _currentQuestPointer = 0;
        }

        Update();
    }

    private void OnPreviousQuestButtonClicked()
    {
        _currentQuestPointer--;

        if (_currentQuestPointer < 0)
        {
            _currentQuestPointer = _gameData.Quests.Length - 1;
        }

        Update();
    }

    private void Update()
    {
        UpdateQuestName();
        UpdateAvatar();
        QuestUpdated?.Invoke(CurrentQuest);
    }

    private void UpdateQuestName()
    {
        _emitter.QuestNameText.text = _gameData.Quests.Length > 0 ?
            _gameData.Quests[_currentQuestPointer].Name : DefaultQuestName;
    }

    private void UpdateAvatar()
    {
        _emitter.AvatarImage.sprite = _questsPreview.FirstOrDefault(s => s != null && s.name == CurrentQuest.PreviewPath);
    }
}
