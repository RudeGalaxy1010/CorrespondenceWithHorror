using System.Collections.Generic;

public class AnswerPicker : IInitable, IDeinitable
{
    private DialogueDisplayer _dialogueDisplayer;
    private AnswerPickerEmitter _emitter;

    private List<AnswerButton> _answerButtons;
    private int _currentAnswerButtonIndex;

    public AnswerPicker(DialogueDisplayer dialogueDisplayer, AnswerPickerEmitter emitter)
    {
        _answerButtons = new List<AnswerButton>();
        _currentAnswerButtonIndex = 0;
        _dialogueDisplayer = dialogueDisplayer;
        _emitter = emitter;
    }

    public void Init()
    {
        _dialogueDisplayer.QuestionChanged += OnQuestionChanged;
        OnQuestionChanged(_dialogueDisplayer.CurrentQuestion);
    }

    public void Deinit()
    {
        _dialogueDisplayer.QuestionChanged -= OnQuestionChanged;

        for (int i = 0; i < _answerButtons.Count; i++)
        {
            _answerButtons[i].Clicked -= OnAnswerButtonClicked;
        }
    }

    private void OnQuestionChanged(Question question)
    {
        ClearAnswerButtons();

        if (question.Type != QuestionType.Common)
        {
            return;
        }

        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerButton answerButton = GetOrCreateAnswerButton();
            answerButton.SetAnswer(question.Answers[i]);
            answerButton.SetInteractable(true);
        }
    }

    private void ClearAnswerButtons()
    {
        for (int i = 0; i < _answerButtons.Count; i++)
        {
            _answerButtons[i].gameObject.SetActive(false);
        }

        _currentAnswerButtonIndex = 0;
    }

    private AnswerButton GetOrCreateAnswerButton()
    {
        AnswerButton answerButton;

        if (_currentAnswerButtonIndex < _answerButtons.Count)
        {
            answerButton = _answerButtons[_currentAnswerButtonIndex];
            answerButton.gameObject.SetActive(true);
            _currentAnswerButtonIndex++;
            return answerButton;
        }

        answerButton = UnityEngine.Object.Instantiate(_emitter.ButtonPrefab, _emitter.Container);
        answerButton.Clicked += OnAnswerButtonClicked;
        _answerButtons.Add(answerButton);
        _currentAnswerButtonIndex++;
        return answerButton;
    }

    private void OnAnswerButtonClicked(Answer answer)
    {
        _dialogueDisplayer.DisplayAnswer(answer.Text);
        _dialogueDisplayer.DisplayQuestion(answer.NextQuestionNumber);

        for (int i = 0; i < _answerButtons.Count; i++)
        {
            _answerButtons[i].SetInteractable(false);
        }
    }
}
