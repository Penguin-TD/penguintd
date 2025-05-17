using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Tower Stats")]
    public float range = 4f;
    public float damage = 25f;
    public float fireRate = 1f;
    [SerializeField] private float hunger = 100f;
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerDecreaseRate = 1f;
    public int cost = 50;

    [Header("Target Mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;

    [NonSerialized] public GameObject target;
    private float cooldown = 0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target) {
            transform.right = target.transform.position - transform.position;
            if(cooldown >= fireRate) {
                target.GetComponent<Enemy>().Damage(damage);
                cooldown = 0.0f;
            }
            else {
                cooldown += Time.deltaTime;
            }
            
        }
        
        hunger -= Time.deltaTime * hungerDecreaseRate;
        if (hunger < 0)
        {
            if (gameObject.name.Replace("(Clone)", "").Trim() == "King Penguin")
            {
                GameOverManager.main.GameOver();
            }
            Destroy(gameObject);
        }
    }
}
