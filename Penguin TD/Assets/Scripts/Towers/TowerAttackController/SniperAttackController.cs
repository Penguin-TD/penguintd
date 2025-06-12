using UnityEngine;

public class SniperAttackController : MonoBehaviour, IAttackController
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
        set => _target = _target;
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

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<TowerPlacement>().isPlacing)
        {
            return;
        }
        
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        if(targets.Length > 0) {
            if(_tower.first) {
                float minDistance = Mathf.Infinity;
                int maxIndex = 0;
                GameObject firstTarget = null;

                foreach(GameObject target in targets) {
                    int index = target.GetComponent<Enemy>().currentIndex;
                    float distance = target.GetComponent<Enemy>().distance;

                    if(index > maxIndex || (index == maxIndex && distance < minDistance)) {
                        maxIndex = index;
                        minDistance = distance;
                        firstTarget = target;
                    }
                }
                _target = firstTarget;
            }
            else if(_tower.last) {
                float maxDistance = -Mathf.Infinity;
                int minIndex = int.MaxValue;
                GameObject lastTarget = null;

                foreach(GameObject target in targets) {
                    int index = target.GetComponent<Enemy>().currentIndex;
                    float distance = target.GetComponent<Enemy>().distance;

                    if(index < minIndex || (index == minIndex && distance > maxDistance)) {
                        minIndex = index;
                        maxDistance = distance;
                        lastTarget = target;
                    }
                }
                _target = lastTarget;
            }
            else if(_tower.strong) {
                GameObject strongestTarget = null;
                float maxHealth = 0;

                foreach(GameObject target in targets) {
                    float health = target.GetComponent<Enemy>().health;
                    if(health > maxHealth) {
                        maxHealth = health;
                        strongestTarget = target;
                    }
                }
                _target = strongestTarget;
            }
            else {
                _target = targets[0];
            }
            
            transform.right = _target.transform.position - transform.position;
            if(cooldown >= _tower.fireRate) {
                _target.GetComponent<Enemy>().Damage(_tower.damage);
                cooldown = 0.0f;
            }
            else {
                cooldown += Time.deltaTime;
            }
        }
    }
}
