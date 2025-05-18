using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class SupportPenguin : MonoBehaviour
{
    [System.Serializable]
    class Level
    {
        public float healDelay = 5f;
        public float healPerecent = 0.5f;
    }
    
    [SerializeField] private Level[] levels = new Level[3];
    [SerializeField] private float healAmount = 50f;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private LayerMask towerLayer;

    private float cooldown = 0f;
    
    void Update()
    {
        if (gameObject.GetComponent<TowerPlacement>().isPlacing)
        {
            return;
        }
        int level = gameObject.GetComponent<TowerUpgrades>().currentLevel;
        cooldown += Time.deltaTime;
        if (cooldown > levels[level].healDelay)
        {
            List<Collider2D> targets = new List<Collider2D>();
            Physics2D.OverlapCollider(rangeCollider, targets);
            
            List<Tower> healTargets = new List<Tower>();
            foreach(Collider2D target in targets)
            {
                if (target.gameObject.tag == "Tower")
                {
                    Tower tower = target.gameObject.GetComponent<Tower>();
                    if ((tower.hunger / tower.maxHunger) < levels[level].healPerecent)
                    {
                        healTargets.Add(tower);
                    }
                }
            }

            if (healTargets.Count > 0)
            {
                cooldown = 0f;
                Debug.Log("Number of targets within range: " + healTargets.Count);
            }

            foreach (Tower healTarget in healTargets)
            {
                healTarget.hunger += healAmount / healTargets.Count;
            }
        }
    }
    
    
}
