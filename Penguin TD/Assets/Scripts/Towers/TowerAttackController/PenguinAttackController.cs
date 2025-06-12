using UnityEngine;

public class PenguinAttackController : MonoBehaviour, IAttackController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    void Start()
    {
    }

    void Update()
    {
        if(_target)
        {
            transform.right = _target.transform.position - transform.position;
            if(_tower.cooldown >= _tower.fireRate)
            {
                Bobber projectile = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Bobber>();
                projectile.Direction = (_target.transform.position - transform.position);
                projectile.Damage = _tower.damage;
                projectile.Speed = _tower.projectileSpeed;
                projectile.Tower = _tower;
                _tower.cooldown = 0.0f;
            }
            
        }
    }
}
