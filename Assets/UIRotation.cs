using UnityEngine;

public class UIRotation : MonoBehaviour
{
    [SerializeField] private Transform _centerEye;
    [SerializeField] private float _maxDegreesPerSecond = 120;
    private void LateUpdate()
    {
        Quaternion newRotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            _centerEye.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _maxDegreesPerSecond * Time.deltaTime);
    }
}
