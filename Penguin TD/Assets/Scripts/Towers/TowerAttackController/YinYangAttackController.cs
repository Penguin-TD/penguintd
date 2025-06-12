using UnityEngine;
using System;
using System.Threading;

public class YinYangAttackController : MonoBehaviour, IAttackController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Tower _tower;
    private GameObject _target;
    private GameObject _projectile;

    public Tower Tower
    {
        get => _tower;
        set => _tower = value;
    }

    public GameObject Target
    {
        get => _target;
        set => _target = value;
    }
    public GameObject Projectile
    {
        get => _projectile;
        set => _projectile = value;
    }
    void Start()
    {
    }

    void Update()
    {
        if(_target)
        {
            Vector3 direction = _target.transform.position - transform.position;
            transform.right = direction;
            if(_tower.cooldown >= _tower.fireRate)
            {
                YinYang projectile = Instantiate(_projectile, _target.transform.position, Quaternion.identity).GetComponent<YinYang>();
                System.Random rng = new System.Random();
                float a = (float)rng.NextDouble();
                float b = (float)rng.NextDouble();
                projectile.transform.up = new Vector3(a, b, 0.0f);
                projectile.Tower = _tower;
                _tower.cooldown = 0.0f;
            }
        }
    }
}