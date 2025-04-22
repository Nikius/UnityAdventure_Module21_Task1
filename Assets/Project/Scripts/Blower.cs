using UnityEngine;

namespace Project.Scripts
{
    public class Blower
    {
        private readonly ParticleSystem _blowVFXPrefab;
        private readonly float _blowRadius;
        private readonly float _blowStrength;
        
        private Vector3 _hitPosition;

        public Blower(float blowStrength, float blowRadius, ParticleSystem blowVFXPrefab)
        {
            _blowStrength = blowStrength;
            _blowRadius = blowRadius;
            _blowVFXPrefab = blowVFXPrefab;
        }

        public void MakeBlow(Ray indicatingRay)
        {
            if (!Physics.Raycast(indicatingRay, out RaycastHit hit))
                return;
            
            Collider[] blownObjectsColliders = Physics.OverlapSphere(hit.point, _blowRadius);
            
            ShowBlowVFX(hit.point);

            foreach (Collider blownObjectCollider in blownObjectsColliders)
            {
                IBlowable blowable = blownObjectCollider.GetComponent<IBlowable>();
                
                if (blowable is not null)
                    blowable.OnBlow(hit.point, _blowStrength);
            }
            
        }

        private void ShowBlowVFX(Vector3 position)
        {
            ParticleSystem blowVFX = Object.Instantiate(_blowVFXPrefab, position, Quaternion.identity);
            blowVFX.Play();
            Object.Destroy(blowVFX.gameObject, blowVFX.main.duration);
        }
    }
}