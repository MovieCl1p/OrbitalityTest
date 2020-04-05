using System;
using System.Collections.Generic;

namespace Core.UI
{
    public class BindableField<T>
    {
        public List<Action<T>> Subscribers;

        private bool _isLocked;
        private T _oldValue;
        private T _value;

        public virtual T Value
        {
            get => _value;
            set
            {
                if (_value == null)
                {
                    _value = value;
                    ExecuteBinding(_value);
                }
                else if (!_value.Equals(value))
                {
                    _value = value;
                    ExecuteBinding(_value);
                }
            }
        }

        public BindableField(T value)
        {
            _value = value;
        }
        
        public void Bind(Action<T> method, bool initialize = true)
        {
            if (Subscribers == null)
            {
                Subscribers = new List<Action<T>>();
            }

            Subscribers.Add(method);

            if (initialize)
            {
                ExecuteBinding(Value);
            }
        }

        public void UnBindAll()
        {
            Subscribers = null;
        }

        public void UnBind(Action<T> method)
        {
            if (Subscribers == null || Subscribers.Count == 0)
            {
                return;
            }

            Subscribers.Remove(method);
        }

        protected virtual void ExecuteBinding(T newValue)
        {
            if (Subscribers == null)
            {
                return;
            }

            for (int i = Subscribers.Count - 1; i >= 0; i--)
            {
                Subscribers[i].Invoke(_isLocked ? _oldValue : newValue);
            }
        }

        public void Retain()
        {
            _isLocked = true;
            _oldValue = Value;
        }

        public void Release()
        {
            _isLocked = false;
            ExecuteBinding(Value);
        }

        public bool Equals(T obj)
        {
            return Value.Equals(obj);
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}