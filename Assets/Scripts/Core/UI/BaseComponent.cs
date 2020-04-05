using System;
using Core.Binder;

namespace Core.UI.Components
{
    public abstract class BaseComponent : MonoScheduledBehaviour
    {
        protected override void Start()
        {
            base.Start();

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
    
    public abstract class BaseComponent<T> : BaseComponent where T : IDataContext
    {
        public T DataContext;

        public void SetContext(T context)
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }
            
            DataContext = context;
            OnContextUpdate(DataContext);
        }

        protected virtual void OnContextUpdate(T context) { }

        protected override void OnReleaseResources()
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }

            base.OnReleaseResources();
        }
    }
}