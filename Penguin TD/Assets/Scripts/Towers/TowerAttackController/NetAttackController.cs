using UnityEngine;

public class NetAttackController : MonoBehaviour, IAttackController
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
    private float cooldown = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(_target)
        {
            Vector3 direction = _target.transform.position - transform.position;
            transform.right = direction;
            if(cooldown >= _tower.fireRate)
            {
                Net projectile = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Net>();
                projectile.transform.up = direction;
                projectile.Direction = direction;
                projectile.Damage = _tower.damage;
                projectile.Speed = _tower.projectileSpeed;
                projectile.Tower = _tower;
                cooldown = 0.0f;
            }
            else {
                cooldown += Time.deltaTime;
            }
        }
    }
}