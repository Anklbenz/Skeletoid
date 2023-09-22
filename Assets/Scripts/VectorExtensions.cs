using UnityEngine;

public static class VectorExtensions
{
    public static float InverseLerp(this Vector3 value, Vector3 min, Vector3 max)
    {
        var direction = max - min;
        var av = value - min;
        return Mathf.Clamp01(Vector3.Dot(av, direction) / Vector3.Dot(direction, direction));
    }
    
    public static float InverseLerp(this Vector2 value, Vector2 min, Vector2 max)
    {
        var direction = max - min;
        var av = value - min;
        return Mathf.Clamp01(Vector2.Dot(av, direction) / Vector2.Dot(direction, direction));
    }
}