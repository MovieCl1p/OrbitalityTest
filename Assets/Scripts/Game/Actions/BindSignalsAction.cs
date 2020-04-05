using Core.Actions;
using Core.Binder;
using Core.Dispatcher;
using Game.Signals;
using Game.UI.Base;

namespace Game.Actions
{
    public class BindSignalsAction : BaseAction
    {
        public override void Execute()
        {
            base.Execute();
            var dispatcher = BindManager.GetInstance<IDispatcher>();
            
            dispatcher.DeclareSignal<StartGameSignal>();
            dispatcher.DeclareSignal<OpenMainMenuSignal>();
            
            Complete();
        }
    }
}