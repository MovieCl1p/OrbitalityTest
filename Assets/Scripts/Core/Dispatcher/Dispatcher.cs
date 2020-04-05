using System;
using System.Collections.Generic;
using Core.Dispatcher.Signals;

namespace Core.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly Dictionary<BindingId, SignalDeclaration> _declarations = new Dictionary<BindingId, SignalDeclaration>();
        private readonly Dictionary<BindingId, SignalSubscription> _subscriptions = new Dictionary<BindingId, SignalSubscription>();

        public void DeclareSignal<T>(string identifier = null)
        {
            var id = new BindingId(typeof(T), identifier);
            var declaration = new SignalDeclaration(id);
            _declarations.Add(declaration.BindingId, declaration);
        }

        public void Subscribe<TSignal>(Action<TSignal> callback, string identifier = null)
        {
            Action<object> wrapperCallback = args => callback((TSignal) args);
            SubscribeInternal(typeof(TSignal), identifier, wrapperCallback);
        }

        private void SubscribeInternal(Type signalType, string identifier, Action<object> callback)
        {
            var id = new BindingId(signalType, identifier);
            var declaration = GetDeclaration(id);
            if (declaration == null)
            {
                throw new Exception(string.Format("No signal declared {0}", signalType));
            }

            SignalSubscription subscription;
            if (!_subscriptions.TryGetValue(id, out subscription))
            {
                subscription = new SignalSubscription(callback, declaration);
                _subscriptions.Add(id, subscription);
            }
            
            declaration.Add(subscription);
        }

        public void Unsubscribe<TSignal>(Action<TSignal> callback, string identifier = null)
        {
            var id = new BindingId(typeof(TSignal), identifier);
            SignalSubscription subscription;

            if (_subscriptions.TryGetValue(id, out subscription))
            {
                _subscriptions.Remove(id);
                subscription.Dispose();
            }
        }

        public void Fire<TSignal>(TSignal signal, string identifier = null) where TSignal : ISignal
        {
            var id = new BindingId(typeof(TSignal), identifier);
            var declaration = GetDeclaration(id);
            declaration.Fire(signal);
        }

        private SignalDeclaration GetDeclaration(BindingId signalId)
        {
            SignalDeclaration handler;

            if (_declarations.TryGetValue(signalId, out handler))
            {
                return handler;
            }

            return null;
        }
    }
}