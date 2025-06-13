using UnityEngine;

public class Net : Bobber
{
    private int count = 0;
    
    class Level
    {
        public int maxPierce = 3;
    }
    [SerializeField] private Level[] levels = new Level[3];
    void Start()
    {
        levels[0] = new Level();
        levels[1] = new Level();
        levels[2] = new Level();
        levels[0].maxPierce = 3;
        levels[1].maxPierce = 5;
        levels[2].maxPierce = 10;
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision) {
        
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