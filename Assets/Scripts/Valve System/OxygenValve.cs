
using System;
using GGJ.Gameplay.Player;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class OxygenValve : BasePickable
    {
        [SerializeField]
        protected ValveState state;

        public ValveState TankState => state;

        public bool IsRecharging => state == ValveState.REPAIR;

        [Header("Oxygen Handling")]

        [SerializeField]
        private float OxygenAmount;

        private float MaximumOxygenAmount = 100f;

        [SerializeField]
        private CarryType Type;


        [SerializeField]
        private bool isBroken;

        [SerializeField]
        private float rechargingTime = 0f;

        [SerializeField]
        private float timeTakenToRecharge;


        public override void Interact()
        {
            base.Interact();

            Debug.Log($"{gameObject.name} is being interacted with");

            OnPlayerInteraction();
        }

        private void OnPlayerInteraction()
        {
            switch(state)
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
                rechargingTime = 0f;

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

        }


        private void Recharging()
        {
            rechargingTime += Time.deltaTime;
            //Debug.Log($"{name} is being repaired for {repairSessionTime}");

            CheckForRechargeComplete();
        }

        private void CheckForRechargeComplete()
        {
            if (rechargingTime > timeTakenToRecharge)
            {
                isBroken = false;

                SetState(ValveState.WORKING);

                rechargingTime = 0f;

                Debug.Log("Repair complete");
            }
        }

        private void Update()
        {
            if (IsRecharging)
            {
                //Currently being repaired
                Recharging();

            }
        }

    }
}
