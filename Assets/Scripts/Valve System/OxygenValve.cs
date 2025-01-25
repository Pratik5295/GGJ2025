
using System;
using GGJ.Gameplay.Player;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class OxygenValve : BasePickable
    {
        [SerializeField]
        protected ValveState valveState;

        public ValveState ValveState => valveState;

        public bool IsRepairing => valveState == ValveState.REPAIR;


        [SerializeField]
        private bool isBroken;

        [SerializeField]
        private float repairSessionTime = 0f;

        [SerializeField]
        private float maxRepairTime;


        public override void Interact()
        {
            base.Interact();

            Debug.Log($"{gameObject.name} is being interacted with");

            OnPlayerInteraction();
        }

        private void OnPlayerInteraction()
        {
            switch(valveState)
            {
                case ValveState.BROKEN:

                    //Check with interaction system if player is carrying oxygen tank
                    var playerHeldItem = PlayerManager.Instance.GetCurrentItem();

                    if (playerHeldItem != null)
                    {

                    }
                    else
                    {
                        Debug.LogWarning("You need to have oxygen tanks");
                    }
                    break;
            }
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
            valveState = _state;

            if (valveState == ValveState.BROKEN)
            {
                isBroken = true;
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
            if (repairSessionTime > maxRepairTime)
            {
                isBroken = false;

                SetState(ValveState.WORKING);

                repairSessionTime = 0f;

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
