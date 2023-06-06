using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _victoryEmoji;
    [SerializeField] private GameObject _defeatEmoji;
    [SerializeField] private ResultText _resultText;
    [SerializeField] private TMP_Text _coinsValueText;
    [SerializeField] private AdsButton _adsButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _againButton;
    [SerializeField] private Button _homeButton;

    private EndGame _endGame;
    private RewardCalculator _rewardCalculator;

    public void Construct(EndGame endGame, RewardCalculator rewardCalculator)
    {
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
        _resultText.SetFromResult(result);
        _coinsValueText.text = $"+{_rewardCalculator.GetReward(result)}";

        if (result == GameResult.Victory)
        {
            RenderVictoryUI();
        }
        else
        {
            RenderDefeatUI();
        }

        _panel.SetActive(true);
    }

    private void RenderVictoryUI()
    {
        _victoryEmoji.SetActive(true);
        _defeatEmoji.SetActive(false);
        _againButton.gameObject.SetActive(true);
    }

    private void RenderDefeatUI()
    {
        _victoryEmoji.SetActive(false);
        _defeatEmoji.SetActive(true);
        _againButton.gameObject.SetActive(false);
    }

    private void OnAdsButtonClicked()
    {
        _adsButton.gameObject.SetActive(false);
        Debug.Log("Ads button clicked");
        // TODO: call ads
    }

    private void OnNextButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void OnAgainButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void OnHomeButtonClicked()
    {
        throw new NotImplementedException();
    }
}
