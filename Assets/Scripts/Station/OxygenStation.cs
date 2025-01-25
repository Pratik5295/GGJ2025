
using GGJ.Gameplay.Interfaces;
using UnityEngine;


namespace GGJ.Gameplay {
    public class OxygenStation : BaseTriggerArea
    {
        [SerializeField]
        private OxygenValve oxygenValve;

        [SerializeField]
        private Transform valveStoreLoc;

        public bool HasCylinder => oxygenValve != null;

        public OxygenValve GetOxygenTank => oxygenValve;

        public void SubmitOxygenValve(OxygenValve _valve)
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

        }


        //TO DO:
        /// Station shows UI, HUD wise that the oxygen cylinder has been picked
    }
}
