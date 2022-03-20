using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 tragetPosition;
    private RectTransform pointerRectTransform;
    private bool isOffScreen;

    // Start is called before the first frame update
    private void Awake()
    {
        this.pointerRectTransform = this.transform.Find("EnemyPointer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.MovePointer();

        this.RotatePointer();
    }

    private void MovePointer()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.target.position);

        this.isOffScreen = screenPosition.x <= 0f ||
                            screenPosition.x >= Screen.width ||
                            screenPosition.y <= 0f ||
                            screenPosition.y >= Screen.height;

        if (this.isOffScreen)
        {
            if (screenPosition.z < 0f)
                screenPosition *= -1;

            Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f) ;

            screenPosition -= centerOfScreen;

            float border = 50f;

            if (screenPosition.x <= -centerOfScreen.x)
                screenPosition.x = -centerOfScreen.x + border;

            if (screenPosition.x >= centerOfScreen.x)
                screenPosition.x = centerOfScreen.x - border;

            if (screenPosition.y <= -centerOfScreen.y)
                screenPosition.y = -centerOfScreen.y + border;

            if (screenPosition.y >= centerOfScreen.y)
                screenPosition.y = centerOfScreen.y - border;

            this.pointerRectTransform.localPosition = screenPosition;
        }
    }

    private void RotatePointer()
    {
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(this.target.position);
        Vector3 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (targetPosition.z < 0f)
                targetPosition *= -1;

        Vector2 dir = (targetPosition - centerOfScreen);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        this.pointerRectTransform.localEulerAngles = new Vector3(0f, 0f, angle);
    }
}
