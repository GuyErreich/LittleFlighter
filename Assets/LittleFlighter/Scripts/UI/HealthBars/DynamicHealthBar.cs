using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LittleFlighter.System;

namespace LittleFlighter.UI.HealthBars
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class DynamicHealthBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image foregroundImage;


        [Header("Settings")]
        [SerializeField] private float updateSpeedSeconds = 0.5f;
        [SerializeField] private float positionOffset = 2f;
        [SerializeField] private float maxDistanceToScale = 30000;

        private Vector3 targetScreenPosition, maxScale;
        private bool isOnScreen;

        public GameObject Target { get; set;}

        private void Awake()
        {
            this.maxScale = this.transform.localScale;

            var rect = this.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(0f, 0f);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0.5f, 0.5f);
        }

        private void OnEnable() {
            foregroundImage.fillAmount = 1f;
        }

        public void HandleHealthChanged(float percentage)
        {
            StartCoroutine(this.ChangeToPercentage(percentage));
        }

        private IEnumerator ChangeToPercentage(float percentage)
        {
            float preChangePercentage = foregroundImage.fillAmount;
            float elapsed = 0f;

            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePercentage, percentage, elapsed / updateSpeedSeconds);
                yield return null;
            }

            foregroundImage.fillAmount = percentage;
        }

        private void LateUpdate()
        {
            this.transform.position = Camera.main.WorldToScreenPoint(this.Target.transform.position + Camera.main.transform.up * 
                                                                    (this.positionOffset + (this.positionOffset * this.GetDistancePercentage())));

            this.Visibile(this.transform.position);

            this.ChangeSizeByDistance();
        }

        private void ChangeSizeByDistance()
        {
            if (!this.isOnScreen)
             return;

            Vector3 newScale;

            newScale.x = Mathf.Clamp(this.maxScale.x - this.GetDistancePercentage(), 0.1f, 1f);
            newScale.y = Mathf.Clamp(this.maxScale.y - this.GetDistancePercentage(), 0.1f, 1f);
            newScale.z = Mathf.Clamp(this.maxScale.z - this.GetDistancePercentage(), 0.1f, 1f);

            this.transform.localScale = newScale;
        }

        private float GetDistancePercentage()
        {
            float distance = Vector3.Distance(Camera.main.transform.position, this.Target.transform.position);
            float distancePercentage = distance / this.maxDistanceToScale;
            return distancePercentage;
        }

        private void Visibile(Vector3 screenPosition)
        {

            this.isOnScreen = screenPosition.x >= 0 && screenPosition.x <= Screen.width &&
                            screenPosition.y >= 0 && screenPosition.y <= Screen.height &&
                            screenPosition.z > 0;

            this.GetComponent<CanvasGroup>().alpha = this.isOnScreen ? 1 : 0;
        }
    }
}
