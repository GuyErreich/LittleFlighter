using UnityEngine;

public class CharacterInputManager : MonoBehaviour {

    
    [SerializeField] private Texture2D cursor;
    [SerializeField] [Range(1, 3)] private float looksensitivityY = 1, looksensitivityX = 1;
    [SerializeField] private float deadzone = 0.05f;
    [SerializeField] private Camera cam;

    private SpacecraftController spacecraftController;

    private PlayerControls controls;
    private PlayerControls.CharacterActions characterInput;

    private bool input_W;
    private Vector2 mouseLook;

    private void Awake() 
    {
        Cursor.SetCursor(this.cursor, new Vector2(this.cursor.width / 2, this.cursor.height / 2),
                            CursorMode.Auto);

        spacecraftController =  this.GetComponent<SpacecraftController>();

        this.controls = new PlayerControls();
        this.characterInput = this.controls.Character;
        
        this.characterInput.Movement.performed += ctx => this.input_W = ctx.ReadValueAsButton();
        this.characterInput.Look.performed += ctx => calculateMouseLook(ctx.ReadValue<Vector2>());
    }

    private void Update() 
    {
        this.spacecraftController.ReceiveInput(this.input_W, this.mouseLook);
    }

    private void calculateMouseLook(Vector2 mousePos)
    {
        // mouseLook.x -= (Screen.width/2);
        // mouseLook.y -= Screen.height/2;  

        // this.mouseLook = mouseLook;

        // print(this.mouseLook.magnitude);

        // this.mouseLook.x *= this.looksensitivityX;
        // this.mouseLook.y *= this.looksensitivityY;

        // this.mouseLook.y = -(this.mouseLook.y);

        Vector2 centerPos = new Vector2(this.cam.pixelWidth / 2, this.cam.pixelHeight / 2);

        Vector2 mouseFromCenter = mousePos - centerPos;

        float clampDistance = this.cam.pixelWidth * deadzone;

        print(mouseFromCenter.magnitude);
        print(clampDistance);

        if (mouseFromCenter.magnitude < clampDistance)
            mouseFromCenter = Vector2.zero;

        this.mouseLook.x = mouseFromCenter.x;
        this.mouseLook.y = -mouseFromCenter.y;

        this.mouseLook = this.mouseLook.normalized;
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDestroy() {
        controls.Disable();
    }
}