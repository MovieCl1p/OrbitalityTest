namespace Game.Unit.Mediator
{
    public interface IDamageApplicable
    {
        string Id { get; }
        void ApplyDamage(int damage);
    }
}