using IJunior.TypedScenes;
using UnityEngine;

public class GameStarter : Starter, ISceneLoadHandler<Quest>
{
    [SerializeField] private DialogueDisplayerEmitter _dialogueDisplayerEmitter;
    [SerializeField] private AnswerPickerEmitter _answerPickerEmitter;
    [SerializeField] private MenuLoaderEmitter _menuLoaderEmitter;

    private Quest _quest;

    public void OnSceneLoaded(Quest quest)
    {
        _quest = quest;
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
    }
}
