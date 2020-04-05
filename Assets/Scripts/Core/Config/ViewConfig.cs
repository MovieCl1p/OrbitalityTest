using System;
using Core.UI;

namespace Core.Config
{
    [Serializable]
    public class ViewConfig
    {
        public string Id;
        public BaseView View;

        public ViewConfig(BaseView baseView)
        {
            View = baseView;
            Id = baseView.name;
        }
    }
}