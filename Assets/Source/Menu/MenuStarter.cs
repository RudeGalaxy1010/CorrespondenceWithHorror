using System.Collections.Generic;
using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    private const string QuestsNamesPath = "GameData";

    [SerializeField] private QuestPickerEmitter _questPickPanelEmitter;
    [SerializeField] private RatingDisplayerEmitter _ratingDisplayerEmitter;
    [SerializeField] private QuestLoaderEmitter _questLoaderEmitter;

    private List<IInitable> _initables;
    private List<IDeinitable> _deinitables;

    private void Awake()
    {
        _initables = new List<IInitable>();
        _deinitables = new List<IDeinitable>();
    }

    private void Start()
    {
        GameData gameData = LoadGameData();
        PlayerData playerData = LoadPlayerData(gameData);

        RatingDisplayer ratingDisplayer = new RatingDisplayer(gameData, playerData, _ratingDisplayerEmitter);
        QuestPicker questPicker = Register(new QuestPicker(gameData, _questPickPanelEmitter));
        QuestLoader questLoader = Register(new QuestLoader(questPicker, _questLoaderEmitter));

        Init();
    }

    private void OnDestroy()
    {
        Deinit();
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

    private T Register<T>(T service)
    {
        if (service is IInitable)
        {
            _initables.Add(service as IInitable);
        }
        if (service is IDeinitable)
        {
            _deinitables.Add(service as IDeinitable);
        }

        return service;
    }

    private void Init()
    {
        for (int i = 0; i < _initables.Count; i++)
        {
            _initables[i].Init();
        }
    }

    private void Deinit()
    {
        for (int i = 0; i < _deinitables.Count; i++)
        {
            _deinitables[i].Deinit();
        }
    }
}
