using System;
using System.Collections.Generic;

namespace Core.Dispatcher.Signals
{
    public class SignalDeclaration : IDisposable
    {
        private readonly List<SignalSubscription> _subscriptions = new List<SignalSubscription>();
        private readonly BindingId _bindingId;

        public BindingId BindingId => _bindingId;

        public SignalDeclaration(BindingId bindingId)
        {
            _bindingId = bindingId;
        }

        public void Fire(ISignal signal)
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                var subscription = _subscriptions[i];
                subscription.Invoke(signal);
            }
        }

        public void Add(SignalSubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        public void Remove(SignalSubscription subscription)
        {
            _subscriptions.Remove(subscription);
        }

        public void Dispose()
        {
            _subscriptions.Clear();
        }
    }
}