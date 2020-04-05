using System;
using Core;
using Core.Actions;
using Core.Binder;
using Core.ViewManager;
using Game.Actions;
using Game.Service.Input;
using UnityEngine;

namespace DefaultNamespace.Commands
{
    public class StartCommand
    {
        private readonly Action _callback;

        public StartCommand(Action callback)
        {
            _callback = callback;
        }
        
        public void Execute()
        {
            ActionQueue workFlow = new ActionQueue();
            workFlow.AddAction(new BindServicesAction());
            workFlow.AddAction(new InvokeMethodAction(InitializeServices));
            workFlow.AddAction(new BindSignalsAction());
            workFlow.AddAction(new RegisterViewsAction());
            workFlow.AddAction(new InvokeMethodAction(OnFinish));
            
            workFlow.Start(OnWorkFlowFail);
        }

        private void OnWorkFlowFail(string error)
        {
            Debug.LogError("Work flow failed" + error);
        }

        private void OnFinish()
        {
            _callback?.Invoke();
        }

        private void InitializeServices()
        {
            var resourceCache = BindManager.GetInstance<IResourceCache>();
            resourceCache.Init();
            
            ViewManager.Instance.Init();
        }
    }
}