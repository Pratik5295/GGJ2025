using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    /// <summary>
    /// Basic script to be placed on valve to give visual representation of 
    /// change in its states
    /// </summary>
    /// 
    [DefaultExecutionOrder(3)]
    public class StationColorChanger : MonoBehaviour
    {
        [SerializeField]
        private Material workingMat;

        [SerializeField]
        private Material brokenMat;


        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private OxygenRefillingStation station;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            station = GetComponent<OxygenRefillingStation>();

            if (station != null)
            {
                station.OnStationStateChangeEvent += OnStateChangeHandler;
                OnStateChangeHandler(station.State);
            }
        }

        private void OnDestroy()
        {
            if (station != null)
            {
                station.OnStationStateChangeEvent -= OnStateChangeHandler;
            }
        }

        private void OnStateChangeHandler(StationState valveState)
        {
            switch (valveState)
            {
                case StationState.WORKING:
                    SetMat(workingMat); 
                    break;

                case StationState.BROKEN: 
                    SetMat(brokenMat);
                    break;


            }
        }

        private void SetMat(Material _mat)
        {
            meshRenderer.material = _mat;
        }

    }
}
