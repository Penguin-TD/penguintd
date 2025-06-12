using UnityEngine;

public class KingAttackController : MonoBehaviour, IAttackController
{
    private Tower _tower;
    private GameObject _target;
    private GameObject _projectile;

    public Tower Tower
    {
        get => _tower;
        set => _tower = value;
    }

    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }
    public GameObject Projectile
    {
        get => _projectile;
        set => _projectile = value;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
