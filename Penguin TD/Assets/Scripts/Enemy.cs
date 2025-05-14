using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Transform checkpoint;
    private int currentIndex = 0;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        checkpoint = EnemyManager.main.checkpoints[currentIndex];
    }

    void Update()
    {
        checkpoint = EnemyManager.main.checkpoints[currentIndex];
        if(Vector2.Distance(checkpoint.transform.position, transform.position) <= 0.1f) {
            if(currentIndex < EnemyManager.main.checkpoints.Length - 1) {
                ++currentIndex;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.linearVelocity = direction * moveSpeed;
    }

}
