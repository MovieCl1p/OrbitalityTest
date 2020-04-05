using System;
using Core.UI.Components;
using UnityEngine;

namespace Game.UI.Game.View
{
    public class BorderComponent : BaseComponent
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}