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
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float projectileSpeed = 20f;
    public float bulletRange = 20.0f;
    [SerializeField] private float hungerDecreaseRate = 1f;
    public int cost = 50;
	public string ID = "penguin";

    [Header("Target Mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;
    public bool support = false;
    public int priority = 0;
    public float cooldown = 0.0f;

    [NonSerialized] public GameObject target;

    // Update is called once per frame
    void Awake()
    {
        hunger = maxHunger;
        cooldown = fireRate - 0.5f;
    }
    
    void Update()
    {
        if (!gameObject.GetComponent<TowerPlacement>().isPlacing)
        {
            if (gameObject.name.Replace("(Clone)", "").Trim() == "King Penguin")
            {
                hunger -= Time.deltaTime * hungerDecreaseRate * Player.main.hungerMultiplier;
            }
            else
            {
                hunger -= Time.deltaTime * hungerDecreaseRate;
            }
            
            if (hunger < 0)
            {
                if (gameObject.name.Replace("(Clone)", "").Trim() == "King Penguin")
                {
                    GameOverManager.main.GameOver();
                }

                Destroy(gameObject);
            }
        }

        cooldown += Time.deltaTime;
    }
    
    
}
