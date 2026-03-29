using UnityEngine;
using UnityEngine.UI;
using Features.Core.Scripts.Interface;

namespace Features.UI.Scripts
{
    public class HealthBarUI : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Slider progressBarImage;

        private IHasProgress hasProgress;

        #endregion

        #region Private Methods

        private void Start()
        {
            hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (hasProgress == null)
            {
                return;
            }

            progressBarImage.value = 1f;
            hasProgress.OnProgressChanged += (sender, args) =>
            {
                progressBarImage.value = args.ProgressAmount;
                if (args.ProgressAmount <= 0f)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            };

            Hide();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}