using UnityEngine;

public class PenguinAttackController : MonoBehaviour
{
    protected Tower _tower;
    protected GameObject _target;
    protected GameObject _projectile;
    protected Vector3 _direction;
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
    
    protected virtual void Update()
    {
        if(_target)
        {
            _direction = _target.transform.position - transform.position;
            transform.right = _direction;
            if(_tower.cooldown >= _tower.fireRate)
            {
                SpawnProjectile();
                _tower.cooldown = 0.0f;
            }
        }
    }

    protected virtual void SpawnProjectile()
    {
        Bobber projectile = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Bobber>();
        projectile.Direction = _direction;
        projectile.Damage = _tower.damage;
        projectile.Speed = _tower.projectileSpeed;
        projectile.BulletRange = _tower.bulletRange;
        projectile.Tower = _tower;
    }
}
