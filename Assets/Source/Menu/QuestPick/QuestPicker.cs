using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestPicker : IInitable, IDeinitable
{
    private const string DefaultQuestName = "-";

    private GameData _gameData;
    private Sprite[]_questsPreview;
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
        UpdateAvatar();
    }

    private void OnPreviousQuestButtonClicked()
    {
        _currentQuestPointer--;

        if (_currentQuestPointer < 0)
        {
            _currentQuestPointer = _gameData.QuestsNames.Length - 1;
        }

        UpdateQuestName();
        UpdateAvatar();
    }

    private void UpdateQuestName()
    {
        _emitter.QuestNameText.text = _gameData.QuestsNames.Length > 0 ?
            _gameData.QuestsNames[_currentQuestPointer] : DefaultQuestName;
    }

    private void UpdateAvatar()
    {
        _emitter.AvatarImage.sprite = _questsPreview.FirstOrDefault(s => s != null && s.name == CurrentQuestName);
    }
}
