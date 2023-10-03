using System.Collections.Generic;

public class PauseHandler
{
    public bool isPaused { get; private set; }

    private readonly List<IPauseSensitive> _pauseSensitives = new();

    public void Register(IPauseSensitive pauseSensitive) =>
        _pauseSensitives.Add(pauseSensitive);

    public void Unregister(IPauseSensitive pauseSensitive) =>
        _pauseSensitives.Remove(pauseSensitive);
    
    public void SetPause(bool isPause) {
        isPaused = isPause;
        foreach (var sensitive in _pauseSensitives)
            sensitive.SetPause(isPause);
    }
}