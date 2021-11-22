using UnityEngine;

namespace UI
{
    public class BannerBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _followingPoint;
        private Vector3 _startEulerRotation;
        private void Start() => 
            _startEulerRotation = transform.rotation.eulerAngles;

        private void Update()
        {
            transform.LookAt(_followingPoint);
            transform.rotation = Quaternion.Euler(_startEulerRotation.x, transform.rotation.eulerAngles.y - 180, _startEulerRotation.z);
        }
    }
}