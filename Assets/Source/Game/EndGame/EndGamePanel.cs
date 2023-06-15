using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
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
    private EndGame _endGame;
    private RewardCalculator _rewardCalculator;
    private GameResult _gameResult;

    public void Construct(SceneLoader sceneLoader, EndGame endGame, RewardCalculator rewardCalculator)
    {
        _sceneLoader = sceneLoader;
        _endGame = endGame;
        _rewardCalculator = rewardCalculator;
        _endGame.GameEnded += OnGameEnded;
    }

    private void OnEnable()
    {
        _adsButton.Clicked += OnAdsButtonClicked;
        _nextButton.onClick.AddListener(OnNextButtonClicked);
        _againButton.onClick.AddListener(OnAgainButtonClicked);
        _homeButton.onClick.AddListener(OnHomeButtonClicked);
    }

    private void OnDisable()
    {
        _adsButton.Clicked -= OnAdsButtonClicked;
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
        _againButton.gameObject.SetActive(false);
        _nextButton.gameObject.SetActive(false);
    }

    private void OnAdsButtonClicked()
    {
        _adsButton.gameObject.SetActive(false);
        Debug.Log("Ads button clicked");
        // TODO: call ads
        UpdateReward(_gameResult, true);
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
