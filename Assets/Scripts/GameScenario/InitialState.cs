public class InitialState : State {
	private readonly IFactory _factory;
	private readonly IInput _input;
	private readonly StorageConfig _storageConfig;
	
    public InitialState(StateSwitcher stateSwitcher, StorageConfig config, IInput input, IFactory factory) : base(stateSwitcher) {
	    _factory = factory;
	    _storageConfig = config;
	    _input = input;
    }

    public override void Enter() {
	    InstantiateSceneObjects();
    }
    
    private void InstantiateSceneObjects() {
	    var map = _storageConfig.GetPrefab(0);
	    var paddlePrefab = _storageConfig.GetPaddlePrefab();

	    _factory.Get(map.environment);
	    var levelInstance = _factory.Get<Level>(map.level);
	    var paddleInstance = _factory.Get(paddlePrefab);
	   // paddleInstance.Initialize(_input);
	    
	    paddleInstance.transform.position = levelInstance.paddleOrigin.position;
    }
}

