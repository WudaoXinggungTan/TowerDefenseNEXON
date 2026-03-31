using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Core.Scripts
{
    public class GameWin : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject directionLightGameObject;

        #endregion

        #region Private Methods

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        }

        private void Instance_OnGameStateChanged(object sender, GameManager.State state)
        {
            if (state == GameManager.State.GameOver)
            {
                directionLightGameObject.GetComponent<Animator>().SetTrigger("GameOver");
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameStateChanged -= Instance_OnGameStateChanged;
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(0);
            GameManager.Instance.ResumeTheGame();
        }

        #endregion
    }
}