using IJunior.TypedScenes;
using UnityEngine;

public class GameStarter : Starter, ISceneLoadHandler<QuestData>
{
    [SerializeField] private DialogueDisplayerEmitter _dialogueDisplayerEmitter;
    [SerializeField] private AnswerPickerEmitter _answerPickerEmitter;
    [SerializeField] private MenuLoaderEmitter _menuLoaderEmitter;

    private Quest _quest;
    private PlayerData _playerData;

    public void OnSceneLoaded(QuestData questData)
    {
        _quest = questData.Quest;
        _playerData = questData.PlayerData;
    }

    protected override void OnStart()
    {
        if (_quest == null)
        {
            Debug.LogError("Failed to load quest");
            return;
        }

        DialogueDisplayer dialogueDisplayer = Register(new DialogueDisplayer(_quest, _dialogueDisplayerEmitter));
        AnswerPicker answerPicker = Register(new AnswerPicker(dialogueDisplayer, _answerPickerEmitter));
        EndGame endGame = Register(new EndGame(dialogueDisplayer));
        MenuLoader menuLoader = Register(new MenuLoader(_menuLoaderEmitter));
        ResultSaver resultSaver = Register(new ResultSaver(_playerData, _quest, endGame, new SaveLoad()));
    }
}
