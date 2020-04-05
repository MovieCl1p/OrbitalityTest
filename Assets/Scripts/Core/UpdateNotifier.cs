using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IUpdateHandler
    {
        bool IsUpdating { get; set; }

        bool IsRegistered { get; set; }

        void OnUpdate();
    }

    public class UpdateNotifier : SingletonBaseMonoBehaviour<UpdateNotifier>
    {
        private static long _lastTickEventHappened = -1;

        private readonly List<IUpdateHandler> _handlers = new List<IUpdateHandler>();

        public void Register(IUpdateHandler updateHandler)
        {
            if (updateHandler.IsUpdating)
            {
                return;
            }

            updateHandler.IsUpdating = true;

            if (updateHandler.IsRegistered)
            {
                return;
            }

            updateHandler.IsRegistered = true;

            _handlers.Add(updateHandler);
        }

        public void UnRegister(IUpdateHandler updateHandler)
        {
            updateHandler.IsUpdating = false;
        }

        protected void Update()
        {
            CallUpdateHappened();
        }

        protected void CallUpdateHappened()
        {
            var currentFrame = Time.frameCount;
            if (_lastTickEventHappened >= currentFrame)
            {
                return;
            }

            _lastTickEventHappened = currentFrame;

            var lastFinishedHandler = -1;

            for (int i = 0; i < _handlers.Count; i++)
            {
                var updateHandler = _handlers[i];

                if (!updateHandler.IsUpdating)
                {
                    lastFinishedHandler = i;
                    continue;
                }

                updateHandler.OnUpdate();

                if (!updateHandler.IsUpdating)
                {
                    lastFinishedHandler = i;
                }
            }

            UnregisterDeadUpdateNotifiers(_handlers, lastFinishedHandler);
        }

        private void UnregisterDeadUpdateNotifiers<T>(List<T> list, int from) where T : IUpdateHandler
        {
            var removesPerFrame = 3;

            for (int i = from; i >= 0; i--)
            {
                var updateHandler = list[i];

                if (!updateHandler.IsUpdating)
                {
                    updateHandler.IsRegistered = false;

                    list.RemoveAt(i);

                    removesPerFrame--;

                    if (removesPerFrame <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}