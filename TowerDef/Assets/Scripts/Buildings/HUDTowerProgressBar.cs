using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class HUDTowerProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image progressBar;
        [SerializeField]
        private GameObject progressBarContainer;

        public void HideProgressBar()
        {
            progressBarContainer.SetActive(false);
        }

        public void ShowProgressBar()
        {
            progressBarContainer.SetActive(true);
        }

        public void SetFillAmount(float fillAmount)
        {
            progressBar.fillAmount = fillAmount;
        }
    }
}