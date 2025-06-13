using UnityEngine;
using System;
using System.Threading;

public class YinYangAttackController : PenguinAttackController
{
    protected override void SpawnProjectile()
    {
        YinYang projectile = Instantiate(_projectile, _target.transform.position, Quaternion.identity).GetComponent<YinYang>();
        System.Random rng = new System.Random();
        float a = (float)rng.NextDouble();
        float b = (float)rng.NextDouble();
        projectile.transform.up = new Vector3(a, b, 0.0f);
        projectile.Tower = _tower;
    }
}