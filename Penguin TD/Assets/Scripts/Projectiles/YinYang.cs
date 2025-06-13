using UnityEngine;

public class YinYang : Bobber
{
    [SerializeField] private int component = 0;
    [SerializeField] private float maxDuration = 10.0f;  
    private float duration = 0.0f;

    // Update is called once per frame
    protected override void Update()
    {
        if (component == 0)
        {
            if (!_tower)
            {
                Destroy(gameObject);
            }
            
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
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (component == 1)
            {
                enemy.moveSpeed *= (float)(1.0f / 4.0f);
                enemy.health *= 1.5f;
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