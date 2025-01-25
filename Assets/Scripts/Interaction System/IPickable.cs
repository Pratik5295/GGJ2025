
using UnityEngine;

namespace GGJ.Gameplay.Interfaces
{
    /// <summary>
    /// Base interface for objects that can be picked
    /// </summary>
    public interface IPickable
    {
        void Pick(Transform _parent,Vector3 _offset);
        void Drop();
    }
}
