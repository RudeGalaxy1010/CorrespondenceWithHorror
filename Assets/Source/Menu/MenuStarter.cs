using System.Collections.Generic;
using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    private const string QuestsNamesPath = "GameData";

    [SerializeField] private QuestPickPanelEmitter _questPickPanelEmitter;
    [SerializeField] private RatingDisplayerEmitter _ratingDisplayerEmitter;

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
        PlayerData playerData = gameData.DefaultPlayerData; // TODO: replace with loading

        QuestPickPanel questPickPanel = new QuestPickPanel(gameData, _questPickPanelEmitter);
        Register(questPickPanel);
        RatingDisplayer ratingDisplayer = new RatingDisplayer(gameData, playerData, _ratingDisplayerEmitter);

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

    private void Register(object obj)
    {
        if (obj is IInitable)
        {
            _initables.Add(obj as IInitable);
        }
        if (obj is IDeinitable)
        {
            _deinitables.Add(obj as IDeinitable);
        }
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
