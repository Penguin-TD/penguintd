using UnityEngine;

public class Bobber : MonoBehaviour
{
    protected Vector3 _direction;
    protected float _speed;
    protected float _damage;
    protected Tower _tower;
    protected float _bulletRange; 
    private int count = 0;

    public Vector3 Direction
    {
        get => _direction; 
        set => _direction = value;
    }

    public float Speed
    {
        get => _speed; 
        set => _speed = value;
    }

    public float Damage
    {
        get => _damage; 
        set => _damage = value;
    }

    public Tower Tower
    {
        get => _tower;
        set => _tower = value;
    }

    public float BulletRange
    {
        get => _bulletRange;
        set => _bulletRange = value;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!_tower)
        {
            Destroy(gameObject);
        }
        
        transform.Translate(Vector3.Normalize(_direction) * _speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(_tower.transform.position, transform.position) > _bulletRange)
        {
            Destroy(gameObject);
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(_damage);
            TowerUpgrades towerUpgrades = _tower.GetComponent<TowerUpgrades>();
            if (++count >= towerUpgrades.levels[towerUpgrades.currentLevel].pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
