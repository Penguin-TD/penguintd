using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class TowerAttack : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    [SerializeField] private Tower Tower;
    [SerializeField] private GameObject Projectile;
    private AttackController attackController;
    void Awake()
    {
        String ScriptName = Tower.ID + "AttackController";
        System.Type MyScriptType = System.Type.GetType (ScriptName + ",Assembly-CSharp");
        attackController = (AttackController)gameObject.AddComponent(MyScriptType);
        if(!attackController)
        {
            Debug.Log("Fallback triggered");
            attackController = (AttackController)gameObject.AddComponent<AttackController>();
        }
        
        attackController.Projectile = Projectile;
        attackController.Tower = Tower;
    }

    // Update is called once per frame
    void Update()
    {
        if(targets.Count > 0) {
            if(Tower.first) {
                float minDistance = Mathf.Infinity;
                int maxIndex = 0;
                GameObject firstTarget = null;

                foreach(GameObject target in targets)
                {
                    if (Tower.priority > 0 && !target.GetComponent<Enemy>().isPriority)
                    {
                        continue;
                    }
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

                foreach(GameObject target in targets) 
                {
                    if (Tower.priority > 0 && !target.GetComponent<Enemy>().isPriority)
                    {
                        continue;
                    }
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

                foreach(GameObject target in targets) 
                {
                    if (Tower.priority > 0 && !target.GetComponent<Enemy>().isPriority)
                    {
                        continue;
                    }
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
