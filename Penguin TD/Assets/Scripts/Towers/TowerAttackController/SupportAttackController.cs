using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SupportAttackController : AttackController
{
    class Level
    {
        public float healDelay = 5f;
        public float healPerecent = 0.5f;
    }
    
    [SerializeField] private Level[] levels = new Level[3];
    [SerializeField] private float healAmount = 50f;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private LayerMask towerLayer;
    
    protected override void Update()
    {
        if (gameObject.GetComponent<TowerPlacement>().isPlacing)
        {
            return;
        }
        int level = gameObject.GetComponent<TowerUpgrades>().currentLevel;
        if (_tower.cooldown > levels[level].healDelay)
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
                _tower.cooldown = 0f;
                _tower.hunger -= healAmount;
            }

            foreach (Tower healTarget in healTargets)
            {
                healTarget.hunger += healAmount / healTargets.Count;
            }
        }
    }
}
