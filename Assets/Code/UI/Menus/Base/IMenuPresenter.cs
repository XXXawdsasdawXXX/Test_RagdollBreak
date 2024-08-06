using System;

namespace Code.UI.Menus.Base
{
    public interface IMenuPresenter
    {
        public void ChangeMenuState(MenuState state, Action onCompleted);
    }
}