using IJunior.TypedScenes;
using UnityEngine;

public class GameStarter : Starter, ISceneLoadHandler<Quest>
{
    [SerializeField] private DialogueDisplayerEmitter _dialogueDisplayerEmitter;
    [SerializeField] private AnswerPickerEmitter _answerPickerEmitter;

    private Quest _quest;

    public void OnSceneLoaded(Quest quest)
    {
        _quest = quest;
    }

    protected override void OnStart()
    {
        DialogueDisplayer dialogueDisplayer = Register(new DialogueDisplayer(_quest, _dialogueDisplayerEmitter));
        AnswerPicker answerPicker = Register(new AnswerPicker(dialogueDisplayer, _answerPickerEmitter));
    }
}
