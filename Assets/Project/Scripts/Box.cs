using System;
using UnityEngine;

namespace Project.Scripts
{
    public class Box : MonoBehaviour, IGrabble, IBlowable
    {
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnGrab()
        {
            transform.localScale += Vector3.one;
        }

        public void OnDrop()
        {
            transform.localScale -= Vector3.one;
        }

        public void OnDrag(Vector3 position)
        {
            transform.position = new Vector3(position.x, transform.position.y, position.z);
        }

        public void OnBlow(Vector3 position, float power)
        {
            PushAway(position, power);
        }
        
        private void PushAway(Vector3 position, float power)
        {
            Vector3 direction = transform.position - position;
            Vector3 force = direction.normalized * 1 / direction.magnitude * power;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
