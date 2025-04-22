using UnityEngine;

namespace Project.Scripts
{
    public class BlowController: MonoBehaviour
    {
        private const int RightMouseButtonKey = 1;
        
        [SerializeField] private ParticleSystem _blowVFXPrefab;
        [SerializeField] private float _blowRadius;
        [SerializeField] private float _blowStrength;
        
        private Blower _blower;

        private void Awake()
        {
            _blower = new Blower(_blowStrength, _blowRadius, _blowVFXPrefab);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(RightMouseButtonKey))
                _blower.MakeBlow(GetCameraRay());
        }

        private Ray GetCameraRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}