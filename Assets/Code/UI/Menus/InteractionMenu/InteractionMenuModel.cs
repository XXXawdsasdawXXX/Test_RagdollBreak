using Code.Interaction;
using Code.UI.Menus.Base;
using UnityEngine;

namespace Code.UI.Menus.InteractionMenu
{
    public class InteractionMenuModel: BaseMenuModel<InteractionMenuModel>
    {
        [SerializeField] private InteractionPoint _interactionPoint;
        public bool IsUsed => _interactionPoint != null && _interactionPoint.IsUsed;
        public Vector2 StartPoint;
        public Vector2 EndPoint;
    }
}