using System;
using UnityEngine;

namespace Code.UI.Menus.Base
{
    public abstract class BaseMenuView: MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] protected RectTransform windowTransform;
        public bool IsActivatedWindow => windowTransform != null && windowTransform.gameObject.activeSelf;

        public abstract void OpenMenu(Action onComplete = null);
        public abstract void CloseMenu(Action onComplete = null);
        
        public virtual void SetButtonsInteractable(bool isInteractable) { }
        
    }
}