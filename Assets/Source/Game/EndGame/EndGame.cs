using System;

public class EndGame : IInitable, IDeinitable
{
    public event Action<GameResult> GameEnded;

    private DialogueDisplayer _dialogueDisplayer;

    public EndGame(DialogueDisplayer dialogueDisplayer)
    {
        _dialogueDisplayer = dialogueDisplayer;
    }

    public void Init()
    {
        _dialogueDisplayer.QuestionChanged += OnQuestionChanged;
    }

    public void Deinit()
    {
        _dialogueDisplayer.QuestionChanged -= OnQuestionChanged;
    }

    private void OnQuestionChanged(Question question)
    {
        if (question.Type == QuestionType.Victory)
        {
            UnityEngine.Debug.Log(GameResult.Victory.ToString());
            GameEnded?.Invoke(GameResult.Victory);
        }
        else if (question.Type == QuestionType.Defeat)
        {
            UnityEngine.Debug.Log(GameResult.Defeat.ToString());
            GameEnded?.Invoke(GameResult.Defeat);
        }
    }
}
