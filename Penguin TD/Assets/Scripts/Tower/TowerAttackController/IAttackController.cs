using UnityEngine;

public interface IAttackController
{
    public Tower Tower { get; set; }
    public GameObject Target { get; set; }
    
}