using IJunior.TypedScenes;
using System;
using UnityEngine;

public class QuestLoader : IInitable, IDeinitable
{
    private const string LoadFailedExceptionMessage = "Quest loading failed";

    private QuestPicker _questPicker;
    private QuestLoaderEmitter _questLoaderEmitter;

    public QuestLoader(QuestPicker questPicker, QuestLoaderEmitter questLoaderEmitter)
    {
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
        Quest quest = _questPicker.CurrentQuest;

        if (quest != null)
        {
            GameScene.Load(quest);
        }
    }
}
