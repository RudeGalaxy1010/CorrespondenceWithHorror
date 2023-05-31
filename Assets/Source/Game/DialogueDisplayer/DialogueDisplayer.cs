using System;
using System.Linq;

public class DialogueDisplayer : IInitable
{
    private const int StartQuestion = 0;

    public event Action<Question> QuestionChanged;

    private Quest _quest;
    private DialogueDisplayerEmitter _emitter;
    private Question _currentQuestion;

    public Question CurrentQuestion => _currentQuestion;

    public DialogueDisplayer(Quest quest, DialogueDisplayerEmitter emitter)
    {
        _quest = quest;
        _emitter = emitter;
    }

    public void Init()
    {
        DisplayQuestion(StartQuestion);
    }

    public void DisplayQuestion(int questionNumber)
    {
        _currentQuestion = _quest.Questions.First(q => q.Number == questionNumber);
        Message message = UnityEngine.Object.Instantiate(_emitter.BotMessagePrefab, _emitter.Container);
        message.Display(_currentQuestion.Text);
        QuestionChanged?.Invoke(_currentQuestion);
    }

    public void DisplayAnswer(string answer)
    {
        Message message = UnityEngine.Object.Instantiate(_emitter.PlayerMessagePrefab, _emitter.Container);
        message.Display(answer);
    }
}
