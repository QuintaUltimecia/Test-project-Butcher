public class EntryPoint
{    
    private ResourcesLoader _resourcesLoader;

    private MainCamera _mainCamera;
    private MainCanvas _mainCanvas;
    private Road _road;
    private RunnerController _runnerController;

    public EntryPoint(ResourcesLoader resourcesLoader)
    {
        _resourcesLoader = resourcesLoader;

        LoadResources();
        CreateInjector();
        Initialize();
        Subs();
        Start();
    }

    private void LoadResources()
    {
        _mainCamera = _resourcesLoader.GetResource<MainCamera>(ResourcesList.MainCamera);
        _mainCanvas = _resourcesLoader.GetResource<MainCanvas>(ResourcesList.MainCanvas);
        _road = _resourcesLoader.GetResource<Road>(ResourcesList.Road);
        _runnerController = _resourcesLoader.GetResource<RunnerController>(ResourcesList.RunnerController);
    }

    private void CreateInjector()
    {
        DIContainer.AddMonoBehaviour(_runnerController);
        DIContainer.AddMonoBehaviour(_road.SplineComputer);
        DIContainer.AddMonoBehaviour(_road);
    }

    private void Initialize()
    {
        GameState gameState = new GameState();
        gameState.Init();
        GameState.Instance.Init();
        GameState.SwitchState<MenuState>();

        _runnerController.Initialize();
        _mainCamera.Initialize();
        _mainCanvas.ComponentsProvider.Get<TapHandler>().Initialize();
    }

    private void Subs()
    {
        _mainCanvas.ComponentsProvider.Get<TapHandler>().OnDraw += () => GameStart();
        _mainCanvas.GetButton<RestartButton>().OnClick += () => Restart();
        _runnerController.OnEmptyRunners += () => GameOver();
    }

    private void Start()
    {
        _runnerController.StartRunners();
    }

    private void GameStart()
    {
        _runnerController.Enable();
        _mainCanvas.GetPanel<MenuPanel>().Disable();

        GameState.SwitchState<PlayState>();
    }

    private void GameOver()
    {
        _mainCanvas.ShowPanel<GameOverPanel>();
        _runnerController.Disable();
    }

    private void Restart()
    {
        _runnerController.StartRunners();

        _mainCanvas.GetPanel<GamePanel>().Enable();
        _mainCanvas.GetPanel<MenuPanel>().Enable();
        _mainCanvas.GetPanel<GameOverPanel>().Disable();

        GameState.SwitchState<MenuState>();
    }
}