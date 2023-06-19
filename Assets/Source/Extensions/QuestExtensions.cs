using System.Linq;
using UnityEngine;

public static class QuestExtensions
{
    public static Sprite SelectHeroAvatar(this Quest quest, Sprite[] heroAvatars)
    {
        return heroAvatars.FirstOrDefault(s => s != null && s.name == quest.PreviewPath);
    }
}
