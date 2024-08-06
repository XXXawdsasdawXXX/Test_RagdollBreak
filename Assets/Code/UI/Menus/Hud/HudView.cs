using System;
using Code.UI.Menus.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menus.Hud
{
    public class HudView : BaseMenuView
    {
        [SerializeField] private Button _restartButton;

        public event Action OnPressRestart;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => OnPressRestart?.Invoke());
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
    }
}