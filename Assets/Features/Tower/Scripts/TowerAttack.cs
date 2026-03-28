using UnityEngine;
using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using Features.Projectile.Scripts;

namespace Features.Tower.Scripts
{
    public class TowerAttack : Attack
    {
        protected override void Attacking()
        {
            IProduct product = projectileFactory.GetProduct(projectileFirePosition.position);

            if (product is ProjectileProduct projectile)
            {
                projectile.Target = CurrentTarget;
            }
        }
    }
}