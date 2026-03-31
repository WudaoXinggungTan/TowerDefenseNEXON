using System;
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
        [SerializeField] private bool showHealthFromStart = true;

        private IHasProgress hasProgress;

        #endregion

        #region Private Methods

        private void OnEnable()
        {
            hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (hasProgress == null)
            {
                return;
            }

            progressBarImage.value = 1f;
            hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

            if (showHealthFromStart)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void HasProgress_OnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            progressBarImage.value = e.ProgressAmount;
            if (e.ProgressAmount <= 0f)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void OnDisable()
        {
            hasProgress.OnProgressChanged -= HasProgress_OnProgressChanged;
        }

        private void Show()
        {
            progressBarImage.gameObject.SetActive(true);
        }

        private void Hide()
        {
            progressBarImage.gameObject.SetActive(false);
        }

        #endregion
    }
}