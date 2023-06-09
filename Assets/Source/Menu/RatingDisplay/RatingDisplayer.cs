public class RatingDisplayer
{
    private const string DefaultRating = "-";

    private GameData _gameData;
    private PlayerData _playerData;
    private RatingDisplayerEmitter _emitter;

    public RatingDisplayer(GameData gameData, PlayerData playerData, RatingDisplayerEmitter emitter)
    {
        _gameData = gameData;
        _playerData = playerData;
        _emitter = emitter;
        UpdateRating();
    }

    private void UpdateRating()
    {
        if (_gameData.Ratings == null || _gameData.Ratings.Length == 0)
        {
            _emitter.RatingText.text = DefaultRating;
            return;
        }

        _emitter.RatingText.text = _playerData.Rating < _gameData.Ratings.Length ? 
            _gameData.Ratings[_playerData.Rating] : _gameData.Ratings[_gameData.Ratings.Length - 1];
    }
}
