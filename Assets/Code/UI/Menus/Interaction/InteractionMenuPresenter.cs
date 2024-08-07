using Code.Interaction;
using Code.UI.Menus.Base;
using UnityEngine;

namespace Code.UI.Menus.InteractionMenu
{
    public class InteractionMenuPresenter: BaseMenuPresenter<InteractionMenuModel,InteractionMenuView>
    {
        [SerializeField] private InteractionJoint _interactionJoint;
        [SerializeField] private InteractionPoint _interactionPoint;
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

        private void OnStartTouch(InteractionJoint interactionJoint)
        {
            _interactionJoint = interactionJoint;
            RefreshCoordinate();
            View.SetElementsPosition(startPos: Model.StartPoint, endPos: Model.EndPoint );
            View.EnableElements(true);
        }

        private void RefreshCoordinate()
        {
            Model.StartPoint = RectTransformUtility
                .WorldToScreenPoint(_camera, _interactionJoint.transform.TransformPoint(_interactionJoint.GetAnchor()));
            Model.EndPoint = RectTransformUtility
                .WorldToScreenPoint(_camera, _interactionPoint.transform.TransformPoint(Vector3.zero));
        }
    }
}