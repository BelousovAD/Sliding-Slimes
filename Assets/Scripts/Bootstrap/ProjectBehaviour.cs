namespace Bootstrap
{
    using Savvy.Bootstrap;
    using Savvy.Interfaces;
    using SavvyServices;

    internal class ProjectBehaviour : ProjectBehaviourBase
    {
        protected override void PostAwake()
        { }

        protected override void RegisterMediationService() =>
            RegisterService<IMediationService>(new CustomMediationService());

        protected override void RegisterPreferencesService() =>
            RegisterService<IPreferencesService>(new CustomPreferencesService());
    }
}
