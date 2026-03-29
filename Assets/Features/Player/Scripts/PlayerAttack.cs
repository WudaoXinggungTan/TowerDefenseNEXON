using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using Features.Projectile.Scripts;

namespace Features.Player.Scripts
{
    public class PlayerAttack : Attack
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