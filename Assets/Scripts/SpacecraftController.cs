using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private float forwardThrust = 300;
    [SerializeField] private float maxForwardSpeed = 100;
    [SerializeField] private float verticalTorqueThrust = 300;
    [SerializeField] private float horizontalTorqueThrust = 300;
    [SerializeField] private float maxAngularVelocity = 360;

    private const string MOUSE_VERTICAL_AXIS_NAME = "Mouse Y";
    private const string MOUSE_HORIZONTAL_AXIS_NAME = "Mouse X";
    private float maxForwardSpeedSquare;
    private bool isTrust = false;
    private Vector2 mouseLook;
    private Rigidbody rb;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        this.rb = this.GetComponent<Rigidbody>();

        this.maxForwardSpeedSquare = Mathf.Pow(this.maxForwardSpeed, 2);
        // For efficiency, we precalculate the square root of the desired max velocity
        // so that we can use the cheaper rigidbody.velocity.sqrMagnitude when checking for max speed

        this.rb.maxAngularVelocity = this.maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        this.TryApplyTorque();
        this.TryApplyThrust();
    }

    private void TryApplyThrust()
    {
        if(!this.isTrust)
            return;

        float maxSpeedDelta = (this.maxForwardSpeedSquare - this.rb.velocity.sqrMagnitude) / this.maxForwardSpeedSquare;

        print(maxSpeedDelta);

        if (maxSpeedDelta <= 0f)
        {
            return;
        }    
        
        this.rb.AddForce(this.transform.forward * maxSpeedDelta * this.forwardThrust, ForceMode.Acceleration);
    }

    private void TryApplyTorque()
    {
        float verticalAxis;
        float horizonalAxis;
        
        verticalAxis = this.mouseLook.y;
        horizonalAxis = this.mouseLook.x;

        if (Mathf.Approximately(verticalAxis, 0f) && Mathf.Approximately(horizonalAxis, 0f))
        {
            return;
        }

        Vector3 verticalTorque = this.transform.right * this.verticalTorqueThrust * verticalAxis;
        Vector3 horizontalTorque = this.transform.up * this.horizontalTorqueThrust * horizonalAxis;

        this.rb.AddTorque(verticalTorque + horizontalTorque);
    }

    // TODO: think about this shit
    // private void TryApplyTorque()
    // {        
    //     float verticalAxis = this.mouseLook.y;
    //     float horizonalAxis = this.mouseLook.x;

    //     this.transform.Rotate(this.transform.right, verticalAxis, Space.World);
    //     this.transform.Rotate(this.transform.up, horizonalAxis, Space.World);
    // }

    // TODO: added boost option
    public void ReceiveInput(bool isTrust, Vector2 mouseLook) {
        this.isTrust = isTrust;
        this.mouseLook = mouseLook;
        // sprintPressed = _sprintPressed;
    }
}
