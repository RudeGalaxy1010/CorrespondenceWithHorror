using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    private const string QuestsNamesPath = "GameData";

    private void Start()
    {
        GameData gameData = LoadGameData();
    }

    private GameData LoadGameData()
    {
        TextAsset questsNamesText = Resources.Load<TextAsset>(QuestsNamesPath);
        return JsonUtility.FromJson<GameData>(questsNamesText.text);
    }
}
