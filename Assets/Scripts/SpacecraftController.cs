using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftController : MonoBehaviour
{
    private float maxForwardSpeedSquare;
    private bool isTrust = false, isDash = false;
    private Vector2 mouseLook, movement;
    private Rigidbody rb;



    [Header("Defense Settings")]
    [SerializeField] private int health;


    [Header("Movement Settings")]
    [SerializeField] private float forwardThrust = 300;
    [SerializeField, Range(1, 3)] private float forwardBoost = 1.5f;
    
    [SerializeField] private float strafeThrust = 100;
    [SerializeField, Range(1, 3)] private float strafeBoost = 1.5f;

    [SerializeField] private float maxForwardSpeed = 100;



    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        this.rb = this.GetComponent<Rigidbody>();

        this.maxForwardSpeedSquare = Mathf.Pow(this.maxForwardSpeed, 2);
    }

    private void FixedUpdate()
    {
        this.TryApplyTorque();
        this.TryApplyThrust();
    }

    private void TryApplyThrust()
    {
        if(!this.isTrust && this.movement.x == 0f)
            return;

        // if(this.movement.magnitude == 0f)
        //     return;

        float maxSpeedDelta = (this.maxForwardSpeedSquare - this.rb.velocity.sqrMagnitude) / this.maxForwardSpeedSquare;

        if (maxSpeedDelta <= 0f)
        {
            return;
        }    
        
        if (this.isTrust)
            this.rb.AddForce(this.transform.forward * maxSpeedDelta * this.forwardThrust, ForceMode.Acceleration);
        
        // this.rb.AddForce(this.transform.forward * maxSpeedDelta * this.forwardThrust * this.movement.y + 
        //                     this.transform.right * maxSpeedDelta * this.strafeThrust * this.movement.x, ForceMode.Acceleration);
        
        Debug.Log(isDash);

        if (isDash)
        {
            this.rb.AddForce(this.transform.right * maxSpeedDelta * this.strafeThrust * this.movement.x * this.strafeBoost, ForceMode.VelocityChange);

            // isDash = false; 
        }
    }

    // TODO: think about this shit
    private void TryApplyTorque()
    {        
        float verticalAxis = this.mouseLook.y;
        float horizonalAxis = this.mouseLook.x;

        this.transform.Rotate(this.transform.right, verticalAxis, Space.World);
        this.transform.Rotate(this.transform.up, horizonalAxis, Space.World);
    }

    // TODO: added boost option
    public void ReceiveInput(bool isTrust, Vector2 movement, Vector2 mouseLook, bool isDash) {
        this.isTrust = isTrust;
        this.movement = movement;
        this.mouseLook = mouseLook;
        this.isDash = isDash;
    }
}
