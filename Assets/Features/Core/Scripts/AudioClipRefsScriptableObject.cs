using System;
using UnityEngine;

namespace Features.Core.Scripts
{
    [CreateAssetMenu(fileName = "AudioClipRefs", menuName = "Scriptable Objects/AudioClipRefs")]
    public class AudioClipRefsScriptableObject : ScriptableObject
    {
        public static AudioClipRefsScriptableObject Instance { get; private set; }

        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        [Header("Projectile SFX")]
        public AudioClip[] projectileHit;
        public AudioClip[] projectileShoot;

        [Header("Enemy SFX")]
        public AudioClip[] enemyDeath;

        [Header("Currency SFX")]
        public AudioClip[] currencyCollect;
        public AudioClip[] currencyDrop;

        [Header("Tower SFX")]
        public AudioClip[] towerSpawn;
    }
}