using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private Tower Tower;
    private TowerAttack towerAttack;
    
    void Start()
    {
        UpdateRange();
        towerAttack = Tower.GetComponent<TowerAttack>();
    }

    public void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Tower.support)
        {
            return;
        }
        if(collision.gameObject.tag == "Enemy")
        {
            towerAttack.targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (Tower.support)
        {
            return;
        }
        if(collision.gameObject.tag == "Enemy") {
            towerAttack.targets.Remove(collision.gameObject);
        }
    }
    public void UpdateRange()
    {
        transform.localScale = new Vector3(Tower.range, Tower.range, Tower.range);
    }
}
