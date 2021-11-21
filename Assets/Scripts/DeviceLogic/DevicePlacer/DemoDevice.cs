using UnityEngine;

namespace DeviceLogic.DevicePlacer
{
    public class DemoDevice: MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _renderer;
        [SerializeField] private Material _transparentMaterial;

        private Material _defaultMaterial;
        
        private void Start()
        {
            _defaultMaterial = _renderer[0].material;
        }

        public void MakeTransparent() => SetMaterial(_transparentMaterial);

        public void MakeNormal() => SetMaterial(_defaultMaterial);

        private void SetMaterial(Material material)
        {
            foreach (var meshRenderer in _renderer) 
                meshRenderer.sharedMaterial = material;
        }
    }
}