using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 tragetPosition, centerOfScreen;
    private RectTransform pointerRectTransform;
    private bool isOffScreen;

    // Start is called before the first frame update
    private void Awake()
    {
        this.pointerRectTransform = this.transform.Find("EnemyPointer").GetComponent<RectTransform>();

        // TODO: change this to use the calculate center of screen function
        this.centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f) ;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: create a function the update the center of screen only when the screen size changes
        // this.CalculateCenterOfScreen();

        this.MovePointer();

        this.RotatePointer();

        this.isVisible();
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

            screenPosition -= this.centerOfScreen;

            float border = 50f;

            if (screenPosition.x <= -this.centerOfScreen.x)
                screenPosition.x = -this.centerOfScreen.x + border;

            if (screenPosition.x >= this.centerOfScreen.x)
                screenPosition.x = this.centerOfScreen.x - border;

            if (screenPosition.y <= -this.centerOfScreen.y)
                screenPosition.y = -this.centerOfScreen.y + border;

            if (screenPosition.y >= this.centerOfScreen.y)
                screenPosition.y = this.centerOfScreen.y - border;

            this.pointerRectTransform.localPosition = screenPosition;
        }
    }

    private void RotatePointer()
    {
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(this.target.position);

        if (targetPosition.z < 0f)
                targetPosition *= -1;

        Vector2 dir = (targetPosition - this.centerOfScreen);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        this.pointerRectTransform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    private void isVisible() 
    {
        // int childIndex =  this.pointerRectTransform.transform.GetSiblingIndex();
        // GameObject enemyPointer =  this.transform.GetChild(childIndex).gameObject;

        GameObject enemyPointer = this.pointerRectTransform.gameObject;

        if(this.isOffScreen)
            enemyPointer.SetActive(true);
        else
            enemyPointer.SetActive(false);
    }
}
