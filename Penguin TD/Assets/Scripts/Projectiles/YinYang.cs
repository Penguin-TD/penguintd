using UnityEngine;

public class YinYang : MonoBehaviour, IProjectile
{
    private Vector3 _direction;
    private float _speed;
    private float _damage;
    private Tower _tower;
    [SerializeField] private int component = 0;
    [SerializeField] private float maxDuration = 10.0f;  
    private float duration = 0.0f;

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public float Speed
    {
        get => _speed; 
        set => _speed = 0;
    }

    public float Damage
    {
        get => _damage; 
        set => _damage = 0;
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
        if (component == 0)
        {
            if (duration >= maxDuration)
            {
                Destroy(gameObject);
            }
            else
            {
                duration += Time.deltaTime;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (component == 1)
            {
                enemy.moveSpeed *= (float)(1.0f / 4.0f);
            }
            else if (component == 2)
            {
                enemy.moveSpeed *= 2.0f;
                enemy.Damage(enemy.health * (1.0f / 5.0f));
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Enemy")
        {
            if (component == 1)
            {
                collision.gameObject.GetComponent<Enemy>().moveSpeed /= (float)(1.0f / 4.0f);
            }
            else if (component == 2)
            {
                collision.gameObject.GetComponent<Enemy>().moveSpeed /= 2.0f;
            }
        }
    }
}