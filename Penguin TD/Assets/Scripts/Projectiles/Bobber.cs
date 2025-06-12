using UnityEngine;

public class Bobber : MonoBehaviour, IProjectile
{
    private Vector3 _direction;
    private float _speed;
    private float _damage;
    private Tower _tower;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(_damage);
            Destroy(gameObject);
        }
    }
}
