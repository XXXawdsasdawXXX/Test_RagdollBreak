using System;
using UnityEngine;

namespace Code.Services
{
    public class FrameSetter: MonoBehaviour
    {
        [SerializeField, Range(30, 60)] private int _targetFrameRate = 30;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}