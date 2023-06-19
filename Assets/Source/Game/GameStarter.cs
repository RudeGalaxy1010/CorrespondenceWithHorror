using IJunior.TypedScenes;
using UnityEngine;

public class GameStarter : Starter, ISceneLoadHandler<LevelData>
{
    [SerializeField] private DialogueDisplayerEmitter _dialogueDisplayerEmitter;
    [SerializeField] private AnswerPickerEmitter _answerPickerEmitter;
    [SerializeField] private MenuLoaderEmitter _menuLoaderEmitter;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private SoundPlayerEmitter _soundPlayerEmitter;
    [SerializeField] private Init _init;

    private LevelData _levelData;
    private SaveLoad _saveLoad;

    public void OnSceneLoaded(LevelData questData)
    {
        _levelData = questData;
    }

    protected override void OnStart()
    {
        if (_levelData.Quest == null)
        {
            Debug.LogError("Failed to load quest");
            return;
        }

        _saveLoad = new SaveLoad(_init);
        SceneLoader sceneLoader = new SceneLoader(_levelData);
        SoundPlayer soundPlayer = new SoundPlayer(_soundPlayerEmitter);
        DialogueDisplayer dialogueDisplayer = Register(new DialogueDisplayer(_levelData.Quest, _levelData.HeroAvatar, 
            _levelData.PlayerAvatar, soundPlayer, _dialogueDisplayerEmitter));
        AnswerPicker answerPicker = Register(new AnswerPicker(dialogueDisplayer, _answerPickerEmitter));
        EndGame endGame = Register(new EndGame(dialogueDisplayer));
        MenuLoader menuLoader = Register(new MenuLoader(sceneLoader, _menuLoaderEmitter));
        EndGamePanel endGamePanel = Register(_endGamePanel);
        ResultSaver resultSaver = Register(
            new ResultSaver(_levelData, _saveLoad, endGame, endGamePanel));
        RewardCalculator rewardCalculator = new RewardCalculator(_levelData.Quest);
        endGamePanel.Construct(sceneLoader, _init, endGame, rewardCalculator);
    }
}
