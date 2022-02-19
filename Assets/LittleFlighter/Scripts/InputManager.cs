using UnityEngine;

public class InputManager : MonoBehaviour {
    
    [SerializeField] private SpacecraftController spacecraftController;

    private PlayerControls controls;
    private PlayerControls.CharacterActions movement;

    private bool input_W;

    private void Awake() {
        spacecraftController =  this.GetComponent<SpacecraftController>();

        this.controls = new PlayerControls();
        this.movement = this.controls.Character;
        
        movement.Movement.performed += ctx => input_W = ctx.ReadValueAsButton();
    }

    private void Update() {
        spacecraftController.ReceiveInput(input_W);
        // mouseLook.ReceiveInput(mouseInput)
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDestroy() {
        controls.Disable();
    }
}