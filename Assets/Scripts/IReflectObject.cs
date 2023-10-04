using UnityEngine;

public interface IReflectObject
{
   public Vector3 direction { get; set; }
   public void Reflect(Vector3 hitNormal);
}
