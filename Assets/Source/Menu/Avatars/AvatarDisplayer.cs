using System;
using System.Linq;
using UnityEngine;

public class AvatarDisplayer : IInitable, IDeinitable
{
    private PlayerData _playerData;
    private Sprite[] _avatars;
    private Shop _shop;
    private AvatarDisplayerEmitter _emitter;

    public AvatarDisplayer(PlayerData playerData, Sprite[] avatars, Shop shop, AvatarDisplayerEmitter emitter)
    {
        _playerData = playerData;
        _avatars = avatars;
        _shop = shop;
        _emitter = emitter;
        UpdateAvatar();
    }

    public Sprite CurrentAvatar => _emitter.AvatarImage.sprite;

    public void Init()
    {
        _shop.AvatarUpdated += OnAvatarUpdated;
    }

    public void Deinit()
    {
        _shop.AvatarUpdated -= OnAvatarUpdated;
    }

    private void OnAvatarUpdated()
    {
        UpdateAvatar();
    }

    private void UpdateAvatar()
    {
        string avatarName = _playerData.CurrentAvatarId.ToString();
        Sprite avatar = _avatars.First(a => a.name == avatarName);
        _emitter.AvatarImage.sprite = avatar;
    }
}
