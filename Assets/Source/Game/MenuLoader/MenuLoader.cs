using IJunior.TypedScenes;

public class MenuLoader : IInitable, IDeinitable
{
    private MenuLoaderEmitter _emitter;

    public MenuLoader(MenuLoaderEmitter emitter)
    {
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
        MenuScene.Load();
    }
}
