using Core.UI.Components;
using Core.ViewManager;

namespace Core.UI
{
    public class BaseView : BaseComponent
    {
        public ViewLayer Layer { get; set; }
        
        public void CloseView()
        {
            ViewManager.ViewManager.Instance.RemoveView(this);
        }
    }
}