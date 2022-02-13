using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTransform;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private void Awake()
    {
        _positionOffset = _followTransform.InverseTransformDirection(transform.position - _followTransform.position);
        _rotationOffset = transform.rotation * Quaternion.Inverse(_followTransform.rotation);
    }

    private void Update()
    {
        transform.position = _followTransform.position + _followTransform.TransformDirection(_positionOffset);
        transform.rotation = _followTransform.rotation * _rotationOffset;
    }
}
