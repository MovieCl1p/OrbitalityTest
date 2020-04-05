using System;

namespace Core.Actions
{
    public interface IAction
    {
        void Execute();
        void Destroy();
        event Action OnComplete;
        event Action<string> OnFail;
    }
}