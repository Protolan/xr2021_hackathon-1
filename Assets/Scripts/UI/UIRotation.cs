using System;
using UnityEngine;

public class UIRotation : MonoBehaviour
{
    [SerializeField] private Transform _centerEye;
    [SerializeField] private float _maxDegreesPerSecond = 120;
    [SerializeField] private float _minTransformRotation = 20f;

    private Quaternion _targetRotation;
    private void Start()
    {
        _targetRotation = GetUpdatedYRotation();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _maxDegreesPerSecond * Time.deltaTime);
        if(Mathf.Abs(transform.rotation.eulerAngles.y - _centerEye.eulerAngles.y) < _minTransformRotation) return;
        _targetRotation = GetUpdatedYRotation();
    }

 

    private Quaternion GetUpdatedYRotation()
    {
        return Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            _centerEye.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);
    }
}
