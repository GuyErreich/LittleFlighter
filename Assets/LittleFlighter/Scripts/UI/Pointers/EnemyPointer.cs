using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace LittleFlighter.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class EnemyPointer : MonoBehaviour
    {
        private Vector2 targetScreenPosition, centerOfScreen;
        private Vector2 ResolutionScreenSizeRatio;
        private bool isOnScreen, isBehind, isRight, isAbove;

        public GameObject Target { get; set;}

        // Start is called before the first frame update
        private void Awake()
        {
            var rect = this.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(0f, 0f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            // rect.lossyScale.Set(rect.lossyScale.x * ((float)Screen.currentResolution.width / Screen.width), rect.lossyScale.y * ((float)Screen.currentResolution.height / Screen.height), rect.lossyScale.z);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            this.centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);

            this.targetScreenPosition = Camera.main.WorldToScreenPoint(this.Target.transform.position);

            this.targetScreenPosition -= this.centerOfScreen;

            this.isBehind = Vector3.Dot(Camera.main.transform.forward, (this.Target.transform.position - Camera.main.transform.position).normalized) < 0 ? true : false;
            
            if(this.isBehind) this.targetScreenPosition *= -1f;

            this.Visibile();

            this.Move();

            this.Rotate();
        }

        private void Move()
        {
            // Convert target's position to screen space
            var dir = (CalculateResolutionScreenRatio(this.targetScreenPosition) - Vector2.zero).normalized;

            var distance = Mathf.Max(this.centerOfScreen.x, this.centerOfScreen.y);

            var point = dir * 100;

            var border = 50f;

            point.x = Mathf.Clamp(point.x, -this.centerOfScreen.x + border, this.centerOfScreen.x - border);
            point.y = Mathf.Clamp(point.y, -this.centerOfScreen.y + border, this.centerOfScreen.y - border);

            // this.transform.localPosition = point;
            this.GetComponent<RectTransform>().anchoredPosition = point;
        }

        private void Rotate()
        {
            if (this.isOnScreen)
                return;
       
            Vector2 dir = (this.targetScreenPosition - Vector2.zero).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            this.transform.eulerAngles = new Vector3(0f, 0f, angle);
        }

        private void Visibile() 
        {

            this.isOnScreen = -this.centerOfScreen.x <= this.targetScreenPosition.x && 
                                this.targetScreenPosition.x <= this.centerOfScreen.x &&
                                -this.centerOfScreen.y <= this.targetScreenPosition.y &&
                                this.targetScreenPosition.y <= this.centerOfScreen.y &&
                                !isBehind;

            this.GetComponent<CanvasGroup>().alpha = this.isOnScreen ? 0 : 1;
        }

        private Vector2 CalculateResolutionScreenRatio(Vector2 screenPosition) 
        {
            var widthRatio = (float)Screen.currentResolution.width / Screen.width;
            var heightRatio = (float)Screen.currentResolution.height / Screen.height;

            screenPosition.x *= widthRatio;
            screenPosition.y *= heightRatio;

            return screenPosition;
        }
    }
}
