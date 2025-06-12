using UnityEngine;

public class Net : MonoBehaviour, IProjectile
{
    private Vector3 _direction;
    private float _speed;
    private float _damage;
    private Tower _tower;
    private int count = 0;
    
    class Level
    {
        public int maxPierce = 3;
    }
    [SerializeField] private Level[] levels = new Level[3];

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
        levels[0] = new Level();
        levels[1] = new Level();
        levels[2] = new Level();
        levels[0].maxPierce = 3;
        levels[1].maxPierce = 5;
        levels[2].maxPierce = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.Normalize(_direction) * _speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(_tower.transform.position, transform.position) > 6)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(_damage);
            if (++count >= levels[_tower.GetComponent<TowerUpgrades>().currentLevel].maxPierce)
            {
                Destroy(gameObject);
            }
        }
    }
}