using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class TowerAttack : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    [SerializeField] private Tower Tower;
    [SerializeField] private GameObject Projectile;
    private IAttackController attackController;
    void Start()
    {
        String ScriptName = Tower.ID + "AttackController";
        System.Type MyScriptType = System.Type.GetType (ScriptName + ",Assembly-CSharp");
        attackController = (IAttackController)gameObject.AddComponent(MyScriptType);
        attackController.Projectile = Projectile;
    }

    // Update is called once per frame
    void Update()
    {
        attackController.Tower = Tower;
        if(targets.Count > 0) {
            if(Tower.first) {
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
                attackController.Target = firstTarget;
            }
            else if(Tower.last) {
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
                attackController.Target = lastTarget;
            }
            else if(Tower.strong) {
                GameObject strongestTarget = null;
                float maxHealth = 0;

                foreach(GameObject target in targets) {
                    float health = target.GetComponent<Enemy>().health;
                    if(health > maxHealth) {
                        maxHealth = health;
                        strongestTarget = target;
                    }
                }
                attackController.Target = strongestTarget;
            }
            else if (Tower.support)
            {
                
            }
            else {
                attackController.Target = targets[0];
            }
        }
        else {
            attackController.Target = null;
        }
    }
}
