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
    public class ValveColorChanger : MonoBehaviour
    {
        [SerializeField]
        private Material workingMat;

        [SerializeField]
        private Material brokenMat;

        [SerializeField]
        private Material repairMat;

        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private GameValve gameValve;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            gameValve = GetComponent<GameValve>();

            if (gameValve != null)
            {
                gameValve.OnStateChangeEvent += OnGameValveStateChangeHandler;
                OnGameValveStateChangeHandler(gameValve,gameValve.State);
            }
        }

        private void OnDestroy()
        {
            if (gameValve != null)
            {
                gameValve.OnStateChangeEvent -= OnGameValveStateChangeHandler;
            }
        }

        private void OnGameValveStateChangeHandler(GameValve valve,ValveState valveState)
        {
            switch (valveState)
            {
                case ValveState.WORKING:
                    SetMat(workingMat); 
                    break;

                case ValveState.BROKEN: 
                    SetMat(brokenMat);
                    break;

                case ValveState.REPAIR:
                    SetMat(repairMat);
                    break;

            }
        }

        private void SetMat(Material _mat)
        {
            meshRenderer.material = _mat;
        }

    }
}
