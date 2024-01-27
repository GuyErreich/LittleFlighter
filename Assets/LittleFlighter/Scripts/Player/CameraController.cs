using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTransform;
    [SerializeField] private float rotationDamping, transformDamping;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    public float RotationDamping 
    {
        get => rotationDamping;
        set => rotationDamping = value;
    }

    public float TransformDamping 
    {
        get => transformDamping;
        set => transformDamping = value;
    }

    private void Awake()
    {
        this._positionOffset = this._followTransform.InverseTransformDirection(this.transform.position - this._followTransform.position);

        this._rotationOffset = this.transform.rotation * Quaternion.Inverse(this._followTransform.rotation);
    }


    // TODO: Test if LateUpdate() is  better option
    private void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,
                                                this._followTransform.position + this._followTransform.TransformDirection(this._positionOffset),
                                                Time.fixedDeltaTime * this.transformDamping);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    this._followTransform.rotation * _rotationOffset,
                                                    Time.fixedDeltaTime * this.rotationDamping);
    }
}
