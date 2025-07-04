using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float health = 50.0f;
    public float moveSpeed = 2.0f;
    [SerializeField] private int moneyOnDeath = 50;
    public bool isPriority = false;
    private Rigidbody2D rb;
    private Transform checkpoint;
    public string ID = "Fish";

    [NonSerialized] public int currentIndex = 0;
    [NonSerialized] public float distance = 0.0f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    
    void Start()
    {
        checkpoint = EnemyManager.main.checkpoints[currentIndex];
    }

    void Update()
    {
        checkpoint = EnemyManager.main.checkpoints[currentIndex];
        distance = Vector2.Distance(transform.position, checkpoint.position);

        if(Vector2.Distance(checkpoint.transform.position, transform.position) <= 0.1f) {
            if(currentIndex < EnemyManager.main.checkpoints.Length - 1) {
                ++currentIndex;
            }
            else
            {
                Player.main.hungerMultiplier *= 1.01f;
                Destroy(gameObject);
            }
        }

        if(health <= 0) {
            Player.main.money += moneyOnDeath;
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void Damage(float damage) {
        health -= damage;
        Transform overlay = transform.Find("Overlay");

        foreach (Transform t in overlay)
        {
            Color tmp = t.GetComponent<SpriteRenderer>().color;
            tmp.a = 1 - health / maxHealth;
            t.GetComponent<SpriteRenderer>().color = tmp;
        }

    }

}
