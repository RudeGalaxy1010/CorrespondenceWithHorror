using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DialogueDisplayer : IInitable
{
    private const int StartQuestion = 0;

    public event Action<Question> QuestionChanged;

    private Quest _quest;
    private DialogueDisplayerEmitter _emitter;
    private Question _currentQuestion;

    public DialogueDisplayer(Quest quest, DialogueDisplayerEmitter emitter)
    {
        _quest = quest;
        _emitter = emitter;
    }

    public Question CurrentQuestion => _currentQuestion;
    private bool IsFirstQuestion => _currentQuestion.Number == StartQuestion;

    public void Init()
    {
        DisplayQuestion(StartQuestion);
    }

    public void DisplayQuestion(int questionNumber)
    {
        _currentQuestion = _quest.Questions.First(q => q.Number == questionNumber);

        if (IsFirstQuestion == true)
        {
            DisplayMessage(_emitter.BotMessagePrefab, _currentQuestion.Text);
            QuestionChanged?.Invoke(_currentQuestion);
            return;
        }

        if (string.IsNullOrEmpty(_currentQuestion.Text) == true)
        {
            QuestionChanged?.Invoke(_currentQuestion);
            return;
        }

        _emitter.StartCoroutine(DisplayQuestionWithDelay());
    }

    private void FocusMessage(RectTransform message)
    {
        _emitter.ScrollRect.normalizedPosition = new Vector2(_emitter.ScrollRect.normalizedPosition.x, 0);
    }

    public void DisplayAnswer(string answer)
    {
        _emitter.StartCoroutine(DisplayAnswerWithDelay(answer));
    }

    private IEnumerator DisplayQuestionWithDelay()
    {
        yield return new WaitForSeconds(1f);
        Message message = DisplayMessage(_emitter.BotMessagePrefab, _currentQuestion.Text);
        yield return new WaitForSeconds(0.1f);
        FocusMessage(message.GetComponent<RectTransform>());
        QuestionChanged?.Invoke(_currentQuestion);
    }

    private IEnumerator DisplayAnswerWithDelay(string answer)
    {
        Message message = DisplayMessage(_emitter.PlayerMessagePrefab, answer);
        yield return new WaitForSeconds(0.1f);
        FocusMessage(message.GetComponent<RectTransform>());
    }

    private Message DisplayMessage(Message prefab, string text)
    {
        Message message = UnityEngine.Object.Instantiate(prefab, _emitter.Container);
        message.Display(text);
        return message;
    }
}
