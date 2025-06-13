using UnityEngine;

public class SniperAttackController : AttackController
{
    // Update is called once per frame
    protected override void Update()
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
            if(_tower.cooldown >= _tower.fireRate) {
                _target.GetComponent<Enemy>().Damage(_tower.damage);
                _tower.cooldown = 0.0f;
            }
        }
    }
}
