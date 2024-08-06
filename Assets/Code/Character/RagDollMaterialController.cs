using System;
using UnityEngine;

namespace Code.Character
{
    public class RagDollMaterialController : MonoBehaviour
    {
        [SerializeField] private MaterialController _materialController;
        [SerializeField] private RagDollStateChecker _ragDollStateChecker;

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _ragDollStateChecker.OnBroken += OnRagDollBroken;
            }
            else
            {
                _ragDollStateChecker.OnBroken -= OnRagDollBroken;
            }
        }

        private void OnRagDollBroken()
        {
            _materialController.SetEnable(false);
        }
    }
}