using System;
using UnityEngine;

namespace Code.Services
{
    public class InputService: MonoBehaviour
    {
        public event Action<Vector3> OnStartTouch;
        public event Action<Vector3> OnTouch;
        public event Action<Vector3> OnEndTouch;
        public Vector3 MousePosition { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnStartTouch?.Invoke(Input.mousePosition);
            }
            
            if (Input.GetMouseButton(0))
            {
                MousePosition = Input.mousePosition;
                OnTouch?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnEndTouch?.Invoke(Input.mousePosition);
            }
        }
    }
}