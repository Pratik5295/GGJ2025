using System;
using GGJ.Managers;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class GameValve : BaseInteractable
    {
        [SerializeField]
        protected ValveState state;

        public ValveState State => state;

        public bool IsRepairing => state == ValveState.REPAIR;

        public Action<GameValve,ValveState> OnStateChangeEvent;

        [SerializeField]
        private bool isBroken;

        [SerializeField]
        private float repairSessionTime = 0f;

        [SerializeField]
        private float maxRepairTime;

        public Animator handleAnim;

        public Animator lightAnim;

        private void Start()
        {
            //TO be removed 
            //SetState(ValveState.BROKEN);
        }

        public override void Interact()
        {
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

                AudioManager.Instance.PlayForegroundSound(4);
            }

            //Animation play
            switch(state)
            {
                case ValveState.BROKEN:
                    handleAnim.SetTrigger("isNotUsing");
                    lightAnim.SetTrigger("isBroken");
                    break;

                case ValveState.WORKING:

                    handleAnim.SetTrigger("isUsing");
                    lightAnim.SetTrigger("isWorking");
                    break;
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

        protected virtual void OnPlayerInteraction()
        {
            switch (state)
            {
                case ValveState.BROKEN:

                    SetState(ValveState.REPAIR);
                    Debug.Log($"{gameObject.name} is being repaired");
                    AudioManager.Instance.PlayForegroundSound(5);
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
