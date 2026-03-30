using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using Features.Projectile.Scripts;
using UnityEngine;

namespace Features.Enemy.Scripts
{
    public class EliteEnemyAttack : Attack
    {
        protected override void Attacking()
        {
            IProduct product = projectileFactory.GetProduct(projectileFirePosition.position, projectileFirePosition.rotation);

            if (product is ProjectileProduct projectile)
            {
                projectile.Target = CurrentTarget;
            }
        }
    }
}