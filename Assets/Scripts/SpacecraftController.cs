using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftController : MonoBehaviour
{
    [SerializeField] private bool _useMouse = true;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _forwardThrust = 300;
    [SerializeField] private float _maxForwardSpeed = 100;
    [SerializeField] private float _verticalTorqueThrust = 300;
    [SerializeField] private float _horizontalTorqueThrust = 300;
    [SerializeField] private float _maxAngularVelocity = 360;

    private const string VERTICAL_AXIS_NAME = "Vertical";
    private const string HORIZONTAL_AXIS_NAME = "Horizontal";
    private const string MOUSE_VERTICAL_AXIS_NAME = "Vertical";
    private const string MOUSE_HORIZONTAL_AXIS_NAME = "Horizontal";

    private float _maxForwardSpeedSquareRoot;

    private void Awake()
    {
        _maxForwardSpeedSquareRoot = Mathf.Sqrt(_maxForwardSpeed);
        // For efficiency, we precalculate the square root of the desired max velocity
        // so that we can use the cheaper _rigidbody.velocity.sqrMagnitude when checking for max speed

        _rigidbody.maxAngularVelocity = _maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        TryApplyTorque();
        TryApplyThrust();
    }

    private void TryApplyThrust()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            return;
        }
        var maxSpeedDelta = (_maxForwardSpeedSquareRoot - _rigidbody.velocity.sqrMagnitude) / _maxForwardSpeedSquareRoot;
        if (maxSpeedDelta <= 0f)
        {
            return;
        }    
        _rigidbody.AddForce(transform.forward * maxSpeedDelta * _forwardThrust, ForceMode.Acceleration);
    }

    private void TryApplyTorque()
    {
        float verticalAxis;
        float horizonalAxis;
        if (_useMouse)
        {
            verticalAxis = Input.GetAxis(MOUSE_VERTICAL_AXIS_NAME);
            horizonalAxis = Input.GetAxis(MOUSE_HORIZONTAL_AXIS_NAME);
        }    
        else
        {
            verticalAxis = Input.GetAxis(VERTICAL_AXIS_NAME);
            horizonalAxis = Input.GetAxis(HORIZONTAL_AXIS_NAME);
        }
        if (Mathf.Approximately(verticalAxis, 0f) && Mathf.Approximately(horizonalAxis, 0f))
        {
            return;
        }
        var verticalTorque = transform.right * _verticalTorqueThrust * verticalAxis;
        var horizontalTorque = transform.up * _horizontalTorqueThrust * horizonalAxis;
        _rigidbody.AddTorque(verticalTorque + horizontalTorque);
    }
}
