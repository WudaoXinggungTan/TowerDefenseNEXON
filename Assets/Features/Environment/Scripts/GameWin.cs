using Features.Core.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Environment.Scripts
{
    public class GameWin : MonoBehaviour
    {
        #region Private Methods

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged += (sender, state) =>
            {
                if (state == GameManager.State.GameOver)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("GameOver");
                }
            };
        }

        #endregion

        #region Public Methods

        public void RestartScene()
        {
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}