using UnityEngine;

public class MenuStarter : Starter
{
    private const string QuestsNamesPath = "GameData";
    private const string AvatarsPath = "AvatarsAtlas";
    private const string QuestsPreviewPath = "HeroesAtlas";

    [SerializeField] private QuestPickerEmitter _questPickPanelEmitter;
    [SerializeField] private RatingDisplayerEmitter _ratingDisplayerEmitter;
    [SerializeField] private QuestLoaderEmitter _questLoaderEmitter;
    [SerializeField] private AvatarDisplayerEmitter _avatarDisplayerEmitter;
    [SerializeField] private ShopEmitter _shopEmitter;
    [SerializeField] private BalanceEmitter _balanceEmitter;
    [SerializeField] private BalanceDisplayerEmitter _balanceDisplayerEmitter;
    [SerializeField] private Init _init;

    private SaveLoad _saveLoad;

    protected override void OnStart()
    {
        string lang = "ru"; // TODO: Get from localization

        _saveLoad = new SaveLoad(_init);
        GameData gameData = LoadGameData(lang);
        PlayerData playerData = LoadPlayerData();
        Sprite[] avatars = LoadAvatars();
        Sprite[] questsPreview = LoadQuestsPreview();

        RatingDisplayer ratingDisplayer = new RatingDisplayer(gameData, playerData, _ratingDisplayerEmitter);
        SceneLoader sceneLoader = Register(new SceneLoader());
        QuestPicker questPicker = Register(new QuestPicker(gameData, questsPreview, _questPickPanelEmitter));
        QuestLocker questLocker = Register(new QuestLocker(playerData, questPicker, _questLoaderEmitter));
        Balance balance = Register(new Balance(playerData, _saveLoad, _init, _balanceEmitter));
        BalanceDisplayer balanceDisplayer = Register(new BalanceDisplayer(balance, _balanceDisplayerEmitter));
        Shop shop = Register(new Shop(playerData, _saveLoad, _init, balance, avatars, _shopEmitter));
        AvatarDisplayer avatarDisplayer = Register(new AvatarDisplayer(playerData, avatars, shop, 
            _avatarDisplayerEmitter));
        QuestLoader questLoader = Register(new QuestLoader(sceneLoader, gameData, playerData, questPicker,
            avatarDisplayer, _questLoaderEmitter));
    }

    private GameData LoadGameData(string lang)
    {
        string fullPath = $"{QuestsNamesPath}_{lang}";
        TextAsset gameDataRaw = Resources.Load<TextAsset>(fullPath);
        return JsonUtility.FromJson<GameData>(gameDataRaw.text);
    }

    private PlayerData LoadPlayerData()
    {
        return _saveLoad.LoadPLayerData();
    }

    private Sprite[] LoadAvatars()
    {
        return Resources.LoadAll<Sprite>(AvatarsPath);
    }

    private Sprite[] LoadQuestsPreview()
    {
        return Resources.LoadAll<Sprite>(QuestsPreviewPath);
    }
}
