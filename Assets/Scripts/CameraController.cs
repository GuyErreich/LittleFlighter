using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTransform;
    [SerializeField] private float rotationDamping, transformDamping;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private void Awake()
    {
        this._positionOffset = this._followTransform.InverseTransformDirection(this.transform.position - this._followTransform.position);

        this._rotationOffset = this.transform.rotation * Quaternion.Inverse(this._followTransform.rotation);
    }

    private void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,
                                                this._followTransform.position + this._followTransform.TransformDirection(this._positionOffset),
                                                Time.deltaTime * this.transformDamping);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    this._followTransform.rotation * _rotationOffset,
                                                    Time.deltaTime * this.rotationDamping);
    }
}
