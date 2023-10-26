public class WorldData
{
    public bool isUnlocked;
    public bool isCompleted;
    public int starsCount;
    public int levelsCount;
    public float bestCompletedTime;
    
    private bool _freshUnlocked;

    public bool freshUnlockedTrigger {
        get
        {
            var value = _freshUnlocked;
            _freshUnlocked = false;
            return value;
        }
        set => _freshUnlocked = value;
    }
}
