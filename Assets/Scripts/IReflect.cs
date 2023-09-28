using UnityEngine;

public interface IReflect
{
   public Vector3 direction { get; set; }
   public void Reflect(Vector3 hitNormal);
}
