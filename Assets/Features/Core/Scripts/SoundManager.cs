using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Core.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        #region Variables

        private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME_KEY = "SoundEffectsVolume";
        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioClipRefsScriptableObject audioClipRefsSO;
        [SerializeField] private float volume = 1f;

        private AudioSource audioSource;

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            audioSource = GetComponent<AudioSource>();
            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME_KEY, 1f);
        }

        private void Start()
        {
            Attack.OnAttack += Attack_OnAttack;
        }

        private void Attack_OnAttack(object sender, EventArgs e)
        {
            PlaySound(audioClipRefsSO.projectileShoot, transform.position);
        }

        private void OnDestroy()
        {
            Attack.OnAttack -= Attack_OnAttack;
        }

        #endregion

        #region Public Methods

        public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
        {
            //AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
            audioSource.PlayOneShot(audioClip, volumeMultiplier * volume);
        }

        public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
        {
            PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
        }

        public void ChangeVolume()
        {
            volume += .1f;
            if (volume >= 1.1f)
            {
                volume = 0f;
            }

            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME_KEY, volume);
            PlayerPrefs.Save();
        }

        public float GetVolume()
        {
            return volume;
        }

        #endregion
    }
}