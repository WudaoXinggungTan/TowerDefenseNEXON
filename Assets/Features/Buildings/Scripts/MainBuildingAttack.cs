using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using Features.Projectile.Scripts;
using UnityEngine;

namespace Features.Buildings.Scripts
{
    public class MainBuildingAttack : Attack
    {
        protected override void Attacking()
        {
            base.Attacking();
            IProduct product = projectileFactory.GetProduct(projectileFirePosition.position, projectileFirePosition.transform.rotation);

            if (product is ProjectileProduct projectile)
            {
                projectile.Target = CurrentTarget;
            }
        }
    }
}