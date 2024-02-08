using UnityEngine;
using UnityEngine.UI;

namespace LittleFlighter.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class EnemyPointer : MonoBehaviour
    {
        private Vector3 targetScreenPosition, centerOfScreen;
        private bool isOnScreen;

        public GameObject Target { get; set;}

        // Start is called before the first frame update
        private void Awake()
        {
            var rect = this.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(0f, 0f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            this.centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            this.targetScreenPosition = Camera.main.WorldToScreenPoint(this.Target.transform.position);

            this.Visibile();

            this.Move();

            this.Rotate();

        }

        private void Move()
        {
            if (this.isOnScreen)
                return;

            var position = this.targetScreenPosition;
            
            if (position.z < 0f)
                position *= -1;

            position -= this.centerOfScreen;

            float border = 50f;

            if (position.x <= -this.centerOfScreen.x)
                position.x = -this.centerOfScreen.x + border;

            if (position.x >= this.centerOfScreen.x)
                position.x = this.centerOfScreen.x - border;

            if (position.y <= -this.centerOfScreen.y)
                position.y = -this.centerOfScreen.y + border;

            if (position.y >= this.centerOfScreen.y)
                position.y = this.centerOfScreen.y - border;

            this.transform.localPosition = position;
        }

        private void Rotate()
        {
            if (this.isOnScreen)
                return;

            var targetPosition = this.targetScreenPosition;

            if (targetPosition.z < 0f)
                    targetPosition *= -1;

            Vector2 dir = (targetPosition - this.centerOfScreen);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            this.transform.eulerAngles = new Vector3(0f, 0f, angle);
        }

        private void Visibile() 
        {
            this.isOnScreen = this.targetScreenPosition.x >= 0f &&
                                this.targetScreenPosition.x <= Screen.width &&
                                this.targetScreenPosition.y >= 0f &&
                                this.targetScreenPosition.y <= Screen.height &&
                                this.targetScreenPosition.z > 0f;

            this.GetComponent<CanvasGroup>().alpha = this.isOnScreen ? 0 : 1;
        }
    }
}
