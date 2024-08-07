using System;
using System.Collections;
using System.Collections.Generic;
using Code.UI.Menus.Base;
using Code.UI.UILine;
using UnityEngine;

namespace Code.UI.Menus.InteractionMenu
{
    public class InteractionMenuView : BaseMenuView
    {
        [SerializeField] private UILineRenderer _lineRenderer;
        [SerializeField] private RectTransform _hand;
        [SerializeField] private RectTransform _lineStartPoint;

        private Vector3 _targetStartPosition;
        private Vector3 _targetEndPosition;

        private const float SPEED = 30f;

        private bool _isEnable;

        private void Awake()
        {
            _lineRenderer.points = new List<Vector2>()
            {
                Vector2.zero,
                Vector2.zero,
            };
        }

        private void Update()
        {
            if (_isEnable)
            {
                RefreshElementPositions();
            }
        }

        public override void OpenMenu(Action onComplete = null)
        {
            windowTransform.gameObject.SetActive(true);
            onComplete?.Invoke();
        }

        public override void CloseMenu(Action onComplete = null)
        {
            windowTransform.gameObject.SetActive(false);
            onComplete?.Invoke();
        }

        public void SetElementsPosition(Vector3 startPos, Vector3 endPos)
        {
            _targetEndPosition = endPos;
            _targetStartPosition = startPos;
        }

        public void EnableElements(bool enable)
        {
            _lineStartPoint.position = _targetStartPosition;
            _hand.position = _targetEndPosition;
            _hand.gameObject.SetActive(enable);
            _lineStartPoint.gameObject.SetActive(enable);
            _lineRenderer.gameObject.SetActive(enable);

            _isEnable = enable;
        }


        private void RefreshElementPositions()
        {
            _lineStartPoint.position =
                Vector3.Lerp(_lineStartPoint.position, _targetStartPosition, SPEED * Time.deltaTime);
            _hand.position = Vector3.Lerp(_hand.position, _targetEndPosition, SPEED * Time.deltaTime);
            _lineRenderer.SetPosition(0, _lineStartPoint.anchoredPosition);
            _lineRenderer.SetPosition(1, _hand.anchoredPosition);
        }
    }
}