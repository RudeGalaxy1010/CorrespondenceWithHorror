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
        _emitter.RatingText.text = _gameData.Ratings.Length > 0 ? 
            _gameData.Ratings[_playerData.Rating] : DefaultRating;
    }
}
