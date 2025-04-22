using System;
using UnityEngine;

namespace Project.Scripts
{
    public class CarryController : MonoBehaviour
    {
        private const int LeftMouseButtonKey = 0;
        
        private Grabber _grabber;

        private void Awake()
        {
            _grabber = new Grabber();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(LeftMouseButtonKey))
                _grabber.PutObject();
            
            if (_grabber.IsCarrying)
                _grabber.Carry(GetCameraRay());

            if (Input.GetMouseButtonDown(LeftMouseButtonKey))
                _grabber.TakeObject(GetCameraRay());
        }

        private Ray GetCameraRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
