using UnityEngine;

namespace Project.Scripts
{
    public class Grabber
    {
        public bool IsCarrying;
        
        private IGrabble _carriedObject;
        
        public void TakeObject(Ray indicatingRay)
        {
            if (!IsCarrying
                && Physics.Raycast(indicatingRay, out RaycastHit hit))
            {
                IGrabble grabbedObject = hit.collider.GetComponent<IGrabble>();

                if (grabbedObject is not null)
                {
                    IsCarrying = true;
                    _carriedObject = grabbedObject;
                    _carriedObject.OnGrab();
                }
            }
        }

        public void PutObject()
        {
            if (IsCarrying)
            {
                IsCarrying = false;
                _carriedObject.OnDrop();
                _carriedObject = null;
            }
        }

        public void Carry(Ray indicatingRay)
        {
            if (!Physics.Raycast(indicatingRay, out RaycastHit hit))
                return;
            
            _carriedObject.OnDrag(hit.point);
        }
    }
}