using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    private PlayerData _playerData;
    private SaveLoad _saveLoad;

    public Balance(PlayerData playerData, SaveLoad saveLoad)
    {
        _playerData = playerData;
        _saveLoad = saveLoad;
    }
}
