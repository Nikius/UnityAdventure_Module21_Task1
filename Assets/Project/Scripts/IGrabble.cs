using UnityEngine;

namespace Project.Scripts
{
    public interface IGrabble
    {
        public void OnGrab();
        public void OnDrop();
        public void OnDrag(Vector3 position);
    }
}