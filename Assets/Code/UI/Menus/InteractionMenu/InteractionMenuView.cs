using System;
using System.Collections.Generic;
using Code.UI.Menus.Base;
using Code.UI.UILine;
using UnityEngine;

namespace Code.UI.Menus.InteractionMenu
{
    public class InteractionMenuView: BaseMenuView
    {
        [SerializeField] private LineRendererHUD _lineRenderer;
        [SerializeField] private RectTransform _hand;
        [SerializeField] private RectTransform _lineStartPoint;

        private void Awake()
        {
            _lineRenderer.points = new List<Vector2>()
            {
                Vector2.zero,
                Vector2.zero,
            };
        }

        public override void OpenMenu(Action onComplete = null)
        {
            windowTransform.gameObject.SetActive(true);            
        }

        public override void CloseMenu(Action onComplete = null)
        {
            windowTransform.gameObject.SetActive(false);            
        }

        public void SetElementsPosition(Vector3 startPos, Vector3 endPos)
        {
            _lineStartPoint.position = startPos;
            _hand.position = endPos;
            _lineRenderer.SetPosition(0,_lineStartPoint.anchoredPosition);
            _lineRenderer.SetPosition(1,_hand.anchoredPosition);
        }

        public void EnableElements(bool enable)
        {
            _hand.gameObject.SetActive(enable);
            _lineStartPoint.gameObject.SetActive(enable);
            _lineRenderer.gameObject.SetActive(enable);
        }
    }
}