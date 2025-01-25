using System;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class GameValve : BaseInteractable
    {
        [SerializeField]
        private ValveState state;

        public ValveState State => state;

        public bool IsRepairing => state == ValveState.REPAIR;

        public Action<GameValve,ValveState> OnStateChangeEvent;

        [SerializeField]
        private bool isBroken;

        [SerializeField]
        private float repairSessionTime = 0f;

        [SerializeField]
        private float maxRepairTime;

        private void Start()
        {
            //TO be removed 
            //SetState(ValveState.BROKEN);
        }

        public override void Interact()
        {
            base.Interact();

            Debug.Log($"{gameObject.name} is being interacted with");

            OnPlayerInteraction();
        }

        public override void ResetInteract()
        {
            base.ResetInteract();

            Debug.Log("Restting valve if not fixed");

            if (isBroken)
            {
                //Is or was broken and not fixed yet

                //Reset repair time
                repairSessionTime = 0f;

                SetState(ValveState.BROKEN);
            }
        }

        private void SetState(ValveState _state)
        {
            state = _state;

            if (state == ValveState.BROKEN)
            {
                isBroken = true;
            }

            OnStateChangeEvent?.Invoke(this,state);
        }

        /// <summary>
        /// Force the valve to break
        /// </summary>
        public void BreakValve()
        {
            SetState(ValveState.BROKEN);
        }

        private void OnPlayerInteraction()
        {
            switch (state)
            {
                case ValveState.BROKEN:

                    SetState(ValveState.REPAIR);
                    Debug.Log($"{gameObject.name} is being repaired");

                    break;

                case ValveState.WORKING:
                    Debug.Log("Object is repaired. No interaction needed");
                    //Maybe force the valve to reveal its health?
                    break;
            }
        }

        private void Repairing()
        {
            repairSessionTime += Time.deltaTime;
            //Debug.Log($"{name} is being repaired for {repairSessionTime}");

            CheckForRepairComplete();
        }

        private void CheckForRepairComplete()
        {
            if(repairSessionTime > maxRepairTime)
            {
                isBroken = false;

                SetState(ValveState.WORKING);

                Debug.Log("Repair complete");
            }
        }

        private void Update()
        {
            if (IsRepairing)
            {
                //Currently being repaired
                Repairing();

            }
        }
    }
}
