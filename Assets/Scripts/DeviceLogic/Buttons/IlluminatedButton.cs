using UnityEngine;

namespace DeviceLogic
{
    public class IlluminatedButton : DeviceButton
    {
        [SerializeField] private Material _illuminateMaterial;

        private MeshRenderer _renderer;
        private Material _defaultMaterial;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _defaultMaterial = _renderer.sharedMaterial;
        }

        public override void Activate()
        {
            Illuminate(true);
        }

        public override void Deactivate()
        {
            Illuminate(false);
        }

        private void Illuminate(bool state) => 
            _renderer.sharedMaterial = state ? _defaultMaterial : _illuminateMaterial;
    }
}