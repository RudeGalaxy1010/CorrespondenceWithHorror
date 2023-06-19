using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour, IInitable, IDeinitable
{
    public event Action<int> RewardChanged;
    public event Action NeedSave;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _victoryEmoji;
    [SerializeField] private GameObject _defeatEmoji;
    [SerializeField] private ResultText _resultText;
    [SerializeField] private TMP_Text _coinsValueText;
    [SerializeField] private AdsButton _adsButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _againButton;
    [SerializeField] private Button _homeButton;

    private SceneLoader _sceneLoader;
    private Init _init;
    private EndGame _endGame;
    private RewardCalculator _rewardCalculator;
    private GameResult _gameResult;

    public void Construct(SceneLoader sceneLoader, Init init, EndGame endGame, RewardCalculator rewardCalculator)
    {
        _sceneLoader = sceneLoader;
        _init = init;
        _endGame = endGame;
        _rewardCalculator = rewardCalculator;
    }

    public void Init()
    {
        _endGame.GameEnded += OnGameEnded;
        _adsButton.Clicked += OnAdsButtonClicked;
        _init.OnRewardDouble += OnRewardDouble;
        _nextButton.onClick.AddListener(OnNextButtonClicked);
        _againButton.onClick.AddListener(OnAgainButtonClicked);
        _homeButton.onClick.AddListener(OnHomeButtonClicked);
    }

    public void Deinit()
    {
        _endGame.GameEnded -= OnGameEnded;
        _adsButton.Clicked -= OnAdsButtonClicked;
        _init.OnRewardDouble -= OnRewardDouble;
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
        _againButton.onClick.RemoveListener(OnAgainButtonClicked);
        _homeButton.onClick.RemoveListener(OnHomeButtonClicked);
    }

    private void OnDestroy()
    {
        if (_endGame != null)
        {
            _endGame.GameEnded -= OnGameEnded;
        }
    }

    private void OnGameEnded(GameResult result)
    {
        _gameResult = result;
        _resultText.SetFromResult(_gameResult);
        UpdateReward(_gameResult, false);

        if (_gameResult == GameResult.Victory)
        {
            RenderVictoryUI();
        }
        else
        {
            RenderDefeatUI();
        }

        _panel.SetActive(true);
    }

    private void OnRewardDouble()
    {
        UpdateReward(_gameResult, true);
    }

    private void UpdateReward(GameResult result, bool isAdsShown)
    {
        int reward = _rewardCalculator.GetReward(result, isAdsShown);
        _coinsValueText.text = $"+{reward}";
        RewardChanged?.Invoke(reward);
    }

    private void RenderVictoryUI()
    {
        _victoryEmoji.SetActive(true);
        _defeatEmoji.SetActive(false);
        _againButton.gameObject.SetActive(true);
        _nextButton.gameObject.SetActive(true);
    }

    private void RenderDefeatUI()
    {
        _victoryEmoji.SetActive(false);
        _defeatEmoji.SetActive(true);
        _againButton.gameObject.SetActive(true);
        _nextButton.gameObject.SetActive(false);
    }

    private void OnAdsButtonClicked()
    {
        _adsButton.gameObject.SetActive(false);
        _init.ShowRewardedAd(AdsTags.DoubleRewardTag);
    }

    private void OnNextButtonClicked()
    {
        NeedSave?.Invoke();
        _sceneLoader.LoadNextQuest();
    }

    private void OnAgainButtonClicked()
    {
        NeedSave?.Invoke();
        _sceneLoader.ReloadQuest();
    }

    private void OnHomeButtonClicked()
    {
        NeedSave?.Invoke();
        _sceneLoader.LoadMenu();
    }
}
