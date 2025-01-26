
using System;
using GGJ.Gameplay.Player;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class OxygenTank : BasePickable
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
        private float timeCounter;

        [SerializeField]
        private float timeToEmpty;

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

            rechargingTime = 0f;

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
                SetState(OxyState.FULL);

                rechargingTime = 0f;

                OxygenAmount = MaximumOxygenAmount;
                Debug.Log("Tank recharge complete");
            }
        }

        private void Start()
        {
            timeCounter = 0f;
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
            timeCounter += Time.deltaTime;

            CheckForCylinderEmpty();
        }

        private void CheckForCylinderEmpty()
        {
            if(timeCounter > timeToEmpty)
            {
                //Tank empty
                SetState(OxyState.EMPTY);
                rechargingTime = 0f;
                timeCounter = 0f;

                Debug.Log("Tank empty");
            }
        }

    }
}
