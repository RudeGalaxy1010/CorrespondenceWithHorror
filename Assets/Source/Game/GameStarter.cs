using IJunior.TypedScenes;
using UnityEngine;

public class GameStarter : Starter, ISceneLoadHandler<QuestLevelData>
{
    [SerializeField] private DialogueDisplayerEmitter _dialogueDisplayerEmitter;
    [SerializeField] private AnswerPickerEmitter _answerPickerEmitter;
    [SerializeField] private MenuLoaderEmitter _menuLoaderEmitter;
    [SerializeField] private EndGamePanel _endGamePanel;

    private QuestLevelData _questLevelData;
    private SaveLoad _saveLoad;

    public void OnSceneLoaded(QuestLevelData questData)
    {
        _questLevelData = questData;
    }

    protected override void OnStart()
    {
        if (_questLevelData.Quest == null)
        {
            Debug.LogError("Failed to load quest");
            return;
        }

        _saveLoad = new SaveLoad();
        SceneLoader sceneLoader = new SceneLoader(_questLevelData);
        DialogueDisplayer dialogueDisplayer = Register(
            new DialogueDisplayer(_questLevelData.Quest, _dialogueDisplayerEmitter));
        AnswerPicker answerPicker = Register(new AnswerPicker(dialogueDisplayer, _answerPickerEmitter));
        EndGame endGame = Register(new EndGame(dialogueDisplayer));
        MenuLoader menuLoader = Register(new MenuLoader(sceneLoader, _menuLoaderEmitter));
        ResultSaver resultSaver = Register(
            new ResultSaver(_questLevelData, endGame, _saveLoad));
        RewardCalculator rewardCalculator = new RewardCalculator(_questLevelData.Quest);
        _endGamePanel.Construct(sceneLoader, endGame, rewardCalculator);
    }
}
