using Core;
using Core.Actions;
using Core.Binder;
using Core.Dispatcher;
using Game.Factories.Unit;
using Game.Service;
using Game.Service.Input;
using Game.Service.NPC;
using Game.Unit.Data;

namespace Game.Actions
{
    public class BindServicesAction : BaseAction
    {
        public override void Execute()
        {
            base.Execute();
            
            BindManager.Bind<IResourceCache>().To<ResourceCache>().ToSingleton();
            BindManager.Bind<IDispatcher>().To<Dispatcher>().ToSingleton();
            BindManager.Bind<IInputService>().To<InputService>().ToSingleton();
            BindManager.Bind<IUnitDataMapper>().To<UnitDataMapper>().ToSingleton();
            BindManager.Bind<IUserService>().To<UserService>().ToSingleton();
            BindManager.Bind<IEnemyDataProviderService>().To<EnemyDataProviderService>().ToSingleton();
            BindManager.Bind<IEffectService>().To<EffectService>().ToSingleton();

            BindFactory();
            Complete();
        }

        private void BindFactory()
        {
            BindManager.Bind<IUnitFactory>().To<UnitFactory>().ToSingleton();
        }
    }
}