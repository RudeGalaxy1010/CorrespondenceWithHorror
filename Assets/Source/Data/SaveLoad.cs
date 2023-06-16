using UnityEngine;

public class SaveLoad
{
    private const string PlayerDataKey = "Player";

    private Init _init;

    public SaveLoad(Init init)
    {
        _init = init;
    }

    public void SavePlayerData(PlayerData playerData)
    {
        _init.playerData = playerData;
        _init.Save();
    }

    public PlayerData LoadPLayerData()
    {
        // TODO: Load Save!
        return _init.playerData;
    }
}
