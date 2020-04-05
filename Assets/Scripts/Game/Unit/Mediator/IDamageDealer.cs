using UnityEngine;

namespace Game.Unit.Mediator
{
    public interface IDamageDealer
    {
        void OnTriggerEnter2D(Collider2D other);
    }
}