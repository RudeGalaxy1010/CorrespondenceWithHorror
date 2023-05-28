using UnityEngine;

public class SaveLoad
{
    private const string PlayerDataKey = "Player";

    public bool HasSave => PlayerPrefs.HasKey(PlayerDataKey);

    public void SavePlayerData(PlayerData playerData)
    {
        string data = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PlayerDataKey, data);
    }

    public PlayerData LoadPLayerData()
    {
        string data = PlayerPrefs.GetString(PlayerDataKey);
        return JsonUtility.FromJson<PlayerData>(data);
    }
}
