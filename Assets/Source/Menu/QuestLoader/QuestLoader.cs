using UnityEngine.SceneManagement;

public class QuestLoader : IInitable, IDeinitable
{
    private const int QuestSceneIndex = 1;

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
        string questName = _questPicker.CurrentQuestName;
        // Load scene with parameter
        SceneManager.LoadScene(QuestSceneIndex);
    }
}
