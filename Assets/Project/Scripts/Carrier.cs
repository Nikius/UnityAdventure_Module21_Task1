using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Carrier : MonoBehaviour
    {
        private const int LeftMouseButtonKey = 0;
        private const int RightMouseButtonKey = 1;
        private const float MaxDistance = float.MaxValue;

        [SerializeField] private ParticleSystem _blowVFXPrefab;
        [SerializeField] private LayerMask _movableObjectLayerMask;
        [SerializeField] private float _blowRadius;
        [SerializeField] private float _blowStrength;
        
        private bool _isCarried;
        private GameObject _carriedObject;
        private Vector3 _hitPosition;
        
        private void Update()
        {
            if (Input.GetMouseButtonUp(LeftMouseButtonKey))
                PutObject();
            
            if (_isCarried)
                Carry();

            if (Input.GetMouseButtonDown(LeftMouseButtonKey))
                TakeObject();

            if (Input.GetMouseButtonUp(RightMouseButtonKey))
                MakeBlow();
        }

        private Ray GetCameraRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

        private void TakeObject()
        {
            if (!_isCarried
                && Physics.Raycast(GetCameraRay(), out RaycastHit hit, MaxDistance, _movableObjectLayerMask))
            {
                _isCarried = true;
                _carriedObject = hit.collider.gameObject;
            }
        }
        
        private void PutObject()
        {
            if (_isCarried)
            {
                _isCarried = false;
                _carriedObject = null;
            }
        }

        private void Carry()
        {
            if (!Physics.Raycast(GetCameraRay(), out RaycastHit hit))
                return;
            
            Vector3 hitPosition = hit.point;
            
            _carriedObject.transform.position = new Vector3(hitPosition.x, _carriedObject.transform.position.y, hitPosition.z);
        }

        private void MakeBlow()
        {
            if (!Physics.Raycast(GetCameraRay(), out RaycastHit hit))
                return;
            
            Collider[] blownObjectsColliders = Physics.OverlapSphere(hit.point, _blowRadius, _movableObjectLayerMask);
            
            ShowBlowVFX(hit.point);

            foreach (Collider blownObjectCollider in blownObjectsColliders)
            {
                PushAway(blownObjectCollider, hit.point);
            }
            
        }

        private void PushAway(Collider blownObjectCollider, Vector3 position)
        {
            Rigidbody blownObjectRigidbody = blownObjectCollider.GetComponent<Rigidbody>();
            Vector3 direction = blownObjectCollider.transform.position - position;
            blownObjectRigidbody.AddForce(direction * _blowStrength, ForceMode.Impulse);
        }

        private void ShowBlowVFX(Vector3 position)
        {
            ParticleSystem blowVFX = Instantiate(_blowVFXPrefab, position, Quaternion.identity);
            blowVFX.Play();
            Destroy(blowVFX.gameObject, blowVFX.main.duration);
        }
    }
}
