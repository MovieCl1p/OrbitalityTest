using Core.Actions;
using Core.ViewManager;
using Core.ViewManager.Data;
using Game.UI.Game.View;
using Game.UI.MainMenu.View;

namespace Game.Actions
{
    public class RegisterViewsAction : BaseAction
    {
        public override void Execute()
        {
            base.Execute();
            
            ViewManager.Instance.RegisterView(typeof(MainMenuScreen), LayerType.UILayer);
            ViewManager.Instance.RegisterView(typeof(GameScreen), LayerType.UILayer);
            
            Complete();
        }
    }
}