using Code.UI.Menus.Base;
using UnityEngine.SceneManagement;

namespace Code.UI.Menus.Hud
{
    public class HudPresenter : BaseMenuPresenter<HudModel, HudView>
    {
        protected override void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                View.OnPressRestart += OnPressRestart;
            }
            else
            {
                View.OnPressRestart -= OnPressRestart;
            }
        }

        private void OnPressRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}