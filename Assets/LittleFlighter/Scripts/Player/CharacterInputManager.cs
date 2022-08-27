using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace LittleFlighter
{
    public class CharacterInputManager : MonoBehaviour
    {
        [SerializeField] private Texture2D cursor;
        [SerializeField][Range(1, 3)] private float lookSensitivityY = 1, lookSensitivityX = 1;
        [SerializeField] private float deadzone = 0.05f;
        [SerializeField] private Camera cam;

        private SpacecraftController spacecraftController;
        private ProjectileLauncher projectileLauncher;
        // private SheildAbility sheildAbility;
        private PlayerControls controls;
        private PlayerControls.CharacterActions characterInput;

        private bool input_W, input_LeftMouse, input_Space, input_Dash;
        private Vector2 mouseLook;

        #region Getters and Setters
        public float LookSensitivityX
        {
            get { return this.lookSensitivityX; }
        }
        #endregion

        private void Awake()
        {
            Cursor.SetCursor(this.cursor, new Vector2(this.cursor.width / 2, this.cursor.height / 2),
                                CursorMode.Auto);

            Cursor.lockState = CursorLockMode.Confined;

            spacecraftController = this.GetComponent<SpacecraftController>();
            projectileLauncher = this.GetComponent<ProjectileLauncher>();
            // sheildAbility = this.GetComponent<SheildAbility>();

            this.controls = new PlayerControls();
            this.characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.input_W = ctx.ReadValueAsButton();
            this.characterInput.DashRight.performed += _ => this.spacecraftController.Dash(1);
            this.characterInput.DashLeft.performed += _ => this.spacecraftController.Dash(-1);
            this.characterInput.Look.performed += ctx => this.calculateMouseLook(ctx.ReadValue<Vector2>());
            this.characterInput.Attack.performed += ctx => this.input_LeftMouse = ctx.ReadValueAsButton();
            // this.characterInput.Shield.performed += ctx => this.input_Space = ctx.ReadValueAsButton();
        }

        private void FixedUpdate()
        {
            this.spacecraftController.ReceiveInput(this.input_W, this.mouseLook);
            this.projectileLauncher.ReceiveInput(this.input_LeftMouse);
        }

        /// <summary>
        /// Calculates a normalized value that represents how
        /// far is the mouse from the center of screen.
        /// While using a deadzone that represents an area where the value is 0.
        /// <param name="mousePos">
        ///     the current mouse position
        /// </param>
        /// </summary>
        private void calculateMouseLook(Vector2 mousePos)
        {

            Vector2 centerOfScreen = new Vector2(this.cam.pixelWidth / 2f, this.cam.pixelHeight / 2f);

            Vector2 mouseFromCenter = mousePos / centerOfScreen; // Relative to the middle of the screen in percntage (normalization)

            mouseFromCenter = new Vector2(mouseFromCenter.x - 1f, mouseFromCenter.y - 1f); // Changes the middle of the screen to be (0,0)

            if (Mathf.Abs(mouseFromCenter.x) < deadzone)
                mouseFromCenter.x = 0;

            if (Mathf.Abs(mouseFromCenter.y) < deadzone)
                mouseFromCenter.y = 0;

            this.mouseLook.x = mouseFromCenter.x;
            this.mouseLook.y = -mouseFromCenter.y;

            this.mouseLook.x *= this.lookSensitivityX;
            this.mouseLook.y *= this.lookSensitivityY;
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDestroy()
        {
            controls.Disable();
        }
    }
}