using System;

[Serializable]
public class Question
{
    public int Number;
    public string Text;
    public QuestionType Type;
    public Answer[] Answers;
}
