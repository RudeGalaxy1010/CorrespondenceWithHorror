using System;

[Serializable]
public class Quest
{
    public int Id;
    public string Name;
    public string HeroName;
    public string Task;
    public string PreviewPath;
    public Question[] Questions;
}
