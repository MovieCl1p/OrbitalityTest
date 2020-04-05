using System;

namespace Core.Dispatcher.Signals
{
    public class SignalSubscription : IDisposable
    {
        private readonly Action<object> _callback;
        private readonly SignalDeclaration _declaration;

        public SignalSubscription(Action<object> callback, SignalDeclaration declaration)
        {
            _callback = callback;
            _declaration = declaration;
        }

        public void Dispose()
        {
            _declaration.Remove(this);
        }

        public void Invoke(object signal)
        {
            _callback(signal);
        }
    }
}