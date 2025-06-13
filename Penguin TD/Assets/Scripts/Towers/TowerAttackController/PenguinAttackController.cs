using UnityEngine;
using System.Collections;

public class PenguinAttackController : AttackController
{
    protected override void SpawnProjectile()
    {
        TowerUpgrades towerUpgrades = _tower.GetComponent<TowerUpgrades>();
        if (towerUpgrades.currentLevel == towerUpgrades.levels.Length - 1)
        {
            for (int i = -1; i <= 1; ++i)
            {
                StartCoroutine(InstantiateProjectile(i));
            }
        }
        else
        {
            base.SpawnProjectile();
        }
    }

    IEnumerator InstantiateProjectile(int i)
    {
        yield return new WaitForSecondsRealtime(0.1f * (1 + i));
        Debug.Log("Starting coroutine");
        var projectile = Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<Bobber>();
        projectile.transform.right = _direction;
        var angle = projectile.transform.eulerAngles.z + 30.0f * i;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        projectile.Direction = Quaternion.Euler(0, 0, 30.0f * i) * _direction;
        projectile.Damage = _tower.damage;
        projectile.Speed = _tower.projectileSpeed;
        projectile.BulletRange = _tower.bulletRange;
        projectile.Tower = _tower;
    }
}
