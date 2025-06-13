using UnityEngine;

public class NetAttackController : PenguinAttackController
{
    protected override void SpawnProjectile()
    {
        Net projectile = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Net>();
        projectile.transform.up = _direction;
        projectile.Direction = _direction;
        projectile.Damage = _tower.damage;
        projectile.Speed = _tower.projectileSpeed;
        projectile.Tower = _tower;
        projectile.BulletRange = _tower.bulletRange;
        _tower.cooldown = 0.0f;
    }
}