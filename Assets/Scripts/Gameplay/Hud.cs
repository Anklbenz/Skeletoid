using System;

public class Hud
{
    
    
    private readonly HUDView _viewPrefab;
    private readonly IFactory _factory;
    private HUDView _view;

    public Hud(HUDView viewPrefab, IFactory factory) {
        _viewPrefab = viewPrefab;
        _factory = factory;
    }

    public void Initialize() =>
        _view = _factory.Create(_viewPrefab);
}