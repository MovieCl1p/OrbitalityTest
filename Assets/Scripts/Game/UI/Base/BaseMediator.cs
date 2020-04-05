using System;
using Core.Binder;
using Core.UI;
using Core.ViewManager;

namespace Game.UI.Base
{
    public abstract class BaseMediator
    {
        public abstract void Entry();
        public abstract void Exit();
    }
    
    public abstract class BaseMediator<TView> : BaseMediator where TView : BaseView
    {
        protected TView View;
        
        public override void Entry()
        {
            Inject();
            CreateView();
            OnEntry();
        }

        public override void Exit()
        {
            View.CloseView();
            OnExit();
        }
        
        protected virtual void OnEntry()
        {
        }
        
        protected virtual void OnExit()
        {
        }
        
        private void CreateView()
        {
            if (View == null)
            {
                View = ViewManager.Instance.SetView<TView>();
            }
        }
        
        private void Inject()
        {
            Type t = GetType();

            var fields = t.GetProperties();

            for (int i = 0; i < fields.Length; i++)
            {
                var obj = fields[i].GetCustomAttributes(typeof(Inject), false);
                for (int j = 0; j < obj.Length; j++)
                {
                    Type propertyType = fields[i].PropertyType;
                    fields[i].SetValue(this, BindManager.GetInject(propertyType), null);
                }
            }
        }
    }
}