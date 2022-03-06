using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrosshair : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairTex;
    [SerializeField] private int crosshairSize = 45;
    private Vector2 windowSize;
    Rect crosshairRect;

    // Start is called before the first frame update
    void Start()
    {
        // this.crosshairTex.Resize(2, 2);
        this.windowSize = new Vector2(Screen.width, Screen.height);

        CalculateRect();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.windowSize.x != Screen.width || this.windowSize.y != Screen.height)
            CalculateRect();
    }

    private void CalculateRect()
    {
        this.crosshairRect = new Rect((this.windowSize.x - crosshairSize) / 2f,
                                        (this.windowSize.y - crosshairSize) / 2f,
                                        crosshairSize, crosshairSize);
    }

    private void OnGUI() 
    {
        GUI.DrawTexture(crosshairRect, crosshairTex);
    }
}
