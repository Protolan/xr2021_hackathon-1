using UnityEngine;

namespace UI
{
    public class BannerBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _followingPoint;
        private void LateUpdate() => transform.LookAt(_followingPoint);
    }
}