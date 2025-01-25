
using System;
using GGJ.Gameplay.Player;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class OxygenValve : BasePickable
    {
        [SerializeField]
        protected OxyState state;

        public OxyState TankState => state;

        public bool IsRecharging => state == OxyState.RECHARE;

        public bool IsInUse => state == OxyState.INUSE;

        [Header("Oxygen Handling")]

        [SerializeField]
        private float OxygenAmount;

        private float MaximumOxygenAmount = 100f;

        [SerializeField]
        private CarryType Type;

        public CarryType CurrentType => Type;


        [SerializeField]
        private bool isBroken;

        [SerializeField]
        private float timeToEmpty;

        private float maxTime;

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
                case OxyState.EMPTY:

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

                SetState(OxyState.EMPTY);
            }
        }



        private void SetState(OxyState _state)
        {
            state = _state;


        }


        private void Recharging()
        {
            rechargingTime += Time.deltaTime;

            CheckForRechargeComplete();
        }

        private void CheckForRechargeComplete()
        {
            if (rechargingTime > timeTakenToRecharge)
            {
                isBroken = false;

                SetState(OxyState.FULL);

                rechargingTime = 0f;

                OxygenAmount = MaximumOxygenAmount;

                Type = CarryType.OXY_FULL;

                Debug.Log("Tank recharge complete");
            }
        }

        private void Start()
        {
            maxTime = timeToEmpty;
        }

        private void Update()
        {
            if (IsRecharging)
            {
                //Currently being recharged
                Recharging();
            }
            else if (IsInUse)
            {
                UsingOxygen();
            }
        }

        public void StartRecharging()
        {
            SetState(OxyState.RECHARE);
        }

        public void StartToUse()
        {
            SetState(OxyState.INUSE);
        }
        private void UsingOxygen()
        {
            timeToEmpty += Time.deltaTime;

            CheckForCylinderEmpty();
        }

        private void CheckForCylinderEmpty()
        {
            if(timeToEmpty > maxTime)
            {
                //Tank empty
                SetState(OxyState.EMPTY);
                rechargingTime = 0f;
                timeToEmpty = 0f;

                Type = CarryType.OXY_EMPTY;

                Debug.Log("Tank empty");
            }
        }

    }
}
