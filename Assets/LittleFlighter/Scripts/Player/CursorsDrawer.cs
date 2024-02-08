using Cinemachine.Utility;
using UnityEngine;

public class CursorsDrawer : MonoBehaviour 
{
    [SerializeField] private Texture2D cursor, crosshair, centerOfScreen;
    [SerializeField] private int cursorSize = 45, crosshairSize = 45, centerOfScreenSize = 45;
    [SerializeField] private Color cursorColor = Color.green;   
    [SerializeField] private float crosshairDistance = 200f;

    private Vector2 cursorHotSpot, crosshairHotSpot, centerOfScreenHotSpot; 
    private Vector3 crosshairScreenPosition;

    private void Start() {
        this.cursorHotSpot = new Vector2(this.cursorSize / 2, this.cursorSize / 2);
        this.crosshairHotSpot = new Vector2(this.crosshairSize / 2, this.crosshairSize / 2);
        this.centerOfScreenHotSpot = new Vector2(this.centerOfScreenSize / 2, this.centerOfScreenSize / 2);
    }

    void LateUpdate()
    {
        var crosshairPosition = this.transform.position + this.transform.forward * this.crosshairDistance;
        this.crosshairScreenPosition = Camera.main.WorldToScreenPoint(crosshairPosition);
        this.crosshairScreenPosition.y = Screen.height - this.crosshairScreenPosition.y;
        
    }

    void OnGUI()
    {
        GUI.color = cursorColor;
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - this.cursorHotSpot.x, Event.current.mousePosition.y - this.cursorHotSpot.y, this.cursorSize, this.cursorSize), this.cursor);
        GUI.DrawTexture(new Rect(this.crosshairScreenPosition.x - this.crosshairHotSpot.x, this.crosshairScreenPosition.y - this.crosshairHotSpot.y, crosshairSize, crosshairSize), this.crosshair);
        GUI.DrawTexture(new Rect(Screen.width / 2f - this.centerOfScreenHotSpot.x, Screen.height / 2f - this.centerOfScreenHotSpot.y, this.centerOfScreenSize, this.centerOfScreenSize), this.centerOfScreen);
        GUI.color = Color.white;
    }
}