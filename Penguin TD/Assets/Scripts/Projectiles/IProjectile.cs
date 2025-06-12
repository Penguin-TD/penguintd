using UnityEngine;

public interface IProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Direction { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public Tower Tower { get; set; }
}
