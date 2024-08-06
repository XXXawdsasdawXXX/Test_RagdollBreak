using System;
using UnityEngine;

namespace Code.Services
{
    public class InputService: MonoBehaviour
    {
        public event Action<Vector3> OnStartTouch;
        public event Action<Vector3> OnTouch;
        public event Action<Vector3> OnEndTouch;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnStartTouch?.Invoke(Input.mousePosition);
            }
            
            if (Input.GetMouseButton(0))
            {
                OnTouch?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnEndTouch?.Invoke(Input.mousePosition);
            }
        }
    }
}