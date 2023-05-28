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
        GameData gameData = LoadGameData();
        PlayerData playerData = LoadPlayerData(gameData);
        Sprite[] questsPreview = LoadQuestsPreview();

        RatingDisplayer ratingDisplayer = new RatingDisplayer(gameData, playerData, _ratingDisplayerEmitter);
        QuestPicker questPicker = Register(new QuestPicker(gameData, questsPreview, _questPickPanelEmitter));
        QuestLoader questLoader = Register(new QuestLoader(questPicker, _questLoaderEmitter));
    }

    private GameData LoadGameData()
    {
        TextAsset questsNamesText = Resources.Load<TextAsset>(QuestsNamesPath);
        return JsonUtility.FromJson<GameData>(questsNamesText.text);
    }

    private PlayerData LoadPlayerData(GameData gameData)
    {
        SaveLoad saveLoad = new SaveLoad();

        if (saveLoad.HasSave == true)
        {
            return saveLoad.LoadPLayerData();
        }

        return gameData.DefaultPlayerData;
    }

    private Sprite[] LoadQuestsPreview()
    {
        return Resources.LoadAll<Sprite>(QuestsPreviewPath);
    }
}
