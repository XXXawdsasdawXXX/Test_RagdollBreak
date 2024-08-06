using System;
using Code.Interaction;
using Code.Services;
using Code.UI.Menus.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menus.InteractionMenu
{
    public class InteractionMenuPresenter: BaseMenuPresenter<InteractionMenuModel,InteractionMenuView>
    {
        //[SerializeField] private InputService _inputService;
        [SerializeField] private Transform _interactionObject;
        [SerializeField] private InteractionPoint _interactionPoint;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasScaler _scaler;
        private Camera _camera;

        protected override void Init()
        {
            _camera = Camera.main;
            base.Init();
        }

        protected override void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _interactionPoint.OnStartUse += OnStartTouch;
                _interactionPoint.OnStopUse += OnEndTouch;
            }
            else
            {
                _interactionPoint.OnStartUse += OnStartTouch;
                _interactionPoint.OnStopUse += OnEndTouch;
            }
        }

        private void Update()
        {
            if (Model.IsUsed)
            {
                Move();
            }
        }

        private void OnEndTouch()
        {
            View.EnableElements(false);
        }

        private void Move()
        {
            RefreshCoordinate();
            View.SetElementsPosition(startPos: Model.StartPoint, endPos: Model.EndPoint );
        }

        private void OnStartTouch()
        {
            RefreshCoordinate();
            View.SetElementsPosition(startPos: Model.StartPoint, endPos: Model.EndPoint );
            View.EnableElements(true);
        }

        private void RefreshCoordinate()
        {
       
            /*Model.EndPoint   = new Vector2(UnityEngine.Input.mousePosition.x
                    * _scaler.referenceResolution.x / Screen.width, UnityEngine.Input.mousePosition.y * _scaler.referenceResolution.y / Screen.height);*/
            Model.StartPoint = RectTransformUtility.WorldToScreenPoint(_camera, _interactionObject.transform.TransformPoint(Vector3.zero));
            Model.EndPoint = RectTransformUtility.WorldToScreenPoint(_camera, _interactionPoint.transform.TransformPoint(Vector3.zero));

            //Model.EndPoint = mousePosition / _canvas.scaleFactor;
        }
    }
}