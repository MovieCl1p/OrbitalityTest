using System;

namespace Core.Actions
{
    public class BaseAction : IAction
    {
        public event Action OnComplete;
        public event Action<string> OnFail;

        public virtual void Execute() { }

        protected void Complete()
        {
            OnComplete?.Invoke();
        }

        protected void Fail(string error)
        {
            OnFail?.Invoke(error);
        }

        public virtual void Destroy() { }
    }
}