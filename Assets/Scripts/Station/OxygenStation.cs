
using GGJ.Gameplay.Interfaces;
using UnityEngine;


namespace GGJ.Gameplay {
    public class OxygenStation : BaseTriggerArea
    {
        [SerializeField]
        protected OxygenTank oxygenValve;

        [SerializeField]
        protected Transform valveStoreLoc;

        public bool HasCylinder => oxygenValve != null;

        public OxygenTank GetOxygenTank => oxygenValve;

        [SerializeField]
        private OxygenRefillingStation MainMachine;

        public bool IsMainMachineWorking => MainMachine.State == MetaConstants.EnumManager.StationState.WORKING;

        public virtual void SubmitOxygenValve(OxygenTank _valve)
        {
            oxygenValve = _valve;

            var go = oxygenValve.transform;

            go.SetParent(valveStoreLoc,false);
            go.localPosition = Vector3.zero;
            go.localRotation = Quaternion.identity;
            go.localScale = Vector3.one;

            FillUpOxygenTank();
        }

        public void TakeValve()
        {
            if (oxygenValve == null) return;

            oxygenValve = null;
        }

        public void FillUpOxygenTank()
        {
            oxygenValve.StartRecharging();
        }


        //TO DO:
        /// Station shows UI, HUD wise that the oxygen cylinder has been picked
    }
}
