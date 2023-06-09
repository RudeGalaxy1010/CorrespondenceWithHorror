using UnityEngine;

public class MenuStarter : Starter
{
    private const string QuestsNamesPath = "GameData";
    private const string QuestsPreviewPath = "HeroesAtlas";

    [SerializeField] private QuestPickerEmitter _questPickPanelEmitter;
    [SerializeField] private RatingDisplayerEmitter _ratingDisplayerEmitter;
    [SerializeField] private QuestLoaderEmitter _questLoaderEmitter;

    protected override void OnStart()
    {
        string lang = "ru"; // TODO: Get from localization

        GameData gameData = LoadGameData(lang);
        PlayerData playerData = LoadPlayerData();
        Sprite[] questsPreview = LoadQuestsPreview();

        RatingDisplayer ratingDisplayer = new RatingDisplayer(gameData, playerData, _ratingDisplayerEmitter);
        SceneLoader sceneLoader = Register(new SceneLoader());
        QuestPicker questPicker = Register(new QuestPicker(gameData, questsPreview, _questPickPanelEmitter));
        QuestLoader questLoader = Register(new QuestLoader(sceneLoader, gameData, playerData, questPicker, _questLoaderEmitter));
        QuestLocker questLocker = Register(new QuestLocker(playerData, questPicker, _questLoaderEmitter));
    }

    private GameData LoadGameData(string lang)
    {
        string fullPath = $"{QuestsNamesPath}_{lang}";
        TextAsset questsNamesText = Resources.Load<TextAsset>(fullPath);
        return JsonUtility.FromJson<GameData>(questsNamesText.text);
    }

    private PlayerData LoadPlayerData()
    {
        SaveLoad saveLoad = new SaveLoad();

        if (saveLoad.HasSave == true)
        {
            return saveLoad.LoadPLayerData();
        }

        return new PlayerData();
    }

    private Sprite[] LoadQuestsPreview()
    {
        return Resources.LoadAll<Sprite>(QuestsPreviewPath);
    }
}
