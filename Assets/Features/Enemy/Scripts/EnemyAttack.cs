using Features.Core.Scripts;
using Features.Core.Scripts.Interface;
using Features.Projectile.Scripts;
using UnityEngine;

namespace Features.Enemy.Scripts
{
    public class EnemyAttack : Attack
    {
        [SerializeField] private float enemyDamage;

        protected override void Attacking()
        {
            base.Attacking();

            if (CurrentTarget.GetComponent<IDamageable>() == null)
            {
                return;
            }

            CurrentTarget.GetComponent<IDamageable>().Damage(enemyDamage);
        }
    }
}