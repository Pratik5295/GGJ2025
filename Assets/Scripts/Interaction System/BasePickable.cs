using UnityEngine;
using GGJ.Gameplay.Interfaces;

namespace GGJ.Gameplay
{
    public enum PickState
    {
        DROP = 0,
        HELD = 1
    }

    /// <summary>
    /// Base pickable extends base interactable and pickable
    /// </summary>
    public class BasePickable : BaseInteractable,IPickable
    {
        [SerializeField]
        private PickState state;

        public PickState State => state;

        public void Pick()
        {
            state = PickState.HELD;
        }

        public void Drop()
        {
            state = PickState.DROP;
        }
    }
}
