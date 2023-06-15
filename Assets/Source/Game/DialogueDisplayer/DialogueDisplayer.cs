using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DialogueDisplayer : IInitable
{
    private const int StartQuestion = 0;
    private const float NextQuestionDelay = 1f;
    private const float UpdateFocusDelay = 0.1f;
    private const float EndGameDelay = 4f;

    public event Action<Question> QuestionChanged;

    private Quest _quest;
    private SoundPlayer _soundPlayer;
    private Sprite _heroAvatar;
    private Sprite _playerAvatar;
    private DialogueDisplayerEmitter _emitter;
    private Question _currentQuestion;

    public DialogueDisplayer(Quest quest, Sprite heroAvatar, Sprite playerAvatar, SoundPlayer soundPlayer, 
        DialogueDisplayerEmitter emitter)
    {
        _quest = quest;
        _soundPlayer = soundPlayer;
        _heroAvatar = heroAvatar;
        _playerAvatar = playerAvatar;
        _emitter = emitter;
    }

    public Question CurrentQuestion => _currentQuestion;
    private bool IsFirstQuestion => _currentQuestion.Number == StartQuestion;
    private bool IsLastQuestion => _currentQuestion.Type == QuestionType.Victory 
        || _currentQuestion.Type == QuestionType.Defeat;

    public void Init()
    {
        _emitter.HeroAvatarImage.sprite = _heroAvatar;
        _emitter.HeroNameText.text = _quest.HeroName;
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

        _emitter.StartCoroutine(DisplayQuestionWithDelay(IsLastQuestion));
    }

    private void FocusOnLastMessage()
    {
        _emitter.ScrollRect.normalizedPosition = new Vector2(_emitter.ScrollRect.normalizedPosition.x, 0);
    }

    public void DisplayAnswer(string answer)
    {
        _emitter.StartCoroutine(DisplayAnswerWithDelay(answer));
    }

    private IEnumerator DisplayQuestionWithDelay(bool isLastQuestion = false)
    {
        yield return new WaitForSeconds(NextQuestionDelay);
        DisplayMessage(_emitter.BotMessagePrefab, _currentQuestion.Text);
        yield return new WaitForSeconds(UpdateFocusDelay);
        FocusOnLastMessage();

        if (isLastQuestion == true)
        {
            yield return new WaitForSeconds(EndGameDelay);
        }

        QuestionChanged?.Invoke(_currentQuestion);
    }

    private IEnumerator DisplayAnswerWithDelay(string answer)
    {
        DisplayMessage(_emitter.PlayerMessagePrefab, answer);
        yield return new WaitForSeconds(UpdateFocusDelay);
        FocusOnLastMessage();
    }

    private void DisplayMessage(Message prefab, string text)
    {
        Message message = UnityEngine.Object.Instantiate(prefab, _emitter.Container);
        message.Display(text);
        _soundPlayer.PlayMessageSound();
    }
}
