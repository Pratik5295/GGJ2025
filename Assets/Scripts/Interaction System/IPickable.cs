
using UnityEngine;

namespace GGJ.Gameplay.Interfaces
{
    /// <summary>
    /// Base interface for objects that can be picked
    /// </summary>
    public interface IPickable
    {
        void Pick();
        void Drop();
    }
}
