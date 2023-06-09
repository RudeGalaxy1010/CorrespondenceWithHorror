public class MenuLoader : IInitable, IDeinitable
{
    private SceneLoader _sceneLoader;
    private MenuLoaderEmitter _emitter;

    public MenuLoader(SceneLoader sceneLoader, MenuLoaderEmitter emitter)
    {
        _sceneLoader = sceneLoader;
        _emitter = emitter;
    }

    public void Init()
    {
        _emitter.BackButton.onClick.AddListener(OnBackButtonClicked);
    }

    public void Deinit()
    {
        _emitter.BackButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    private void OnBackButtonClicked()
    {
        _sceneLoader.LoadMenu();
    }
}
