
using GGJ.Gameplay.Interfaces;
using UnityEngine;


namespace GGJ.Gameplay {
    public class OxygenStation : BaseTriggerArea
    {
        [SerializeField]
        private OxygenValve oxygenValve;

        [SerializeField]
        private Transform valveStoreLoc;

        public void SubmitOxygenValve(OxygenValve _valve)
        {
            oxygenValve = _valve;

            var go = oxygenValve.transform;

            go.SetParent(valveStoreLoc);
            go.position = Vector3.zero;
        }

        public void TakeValve()
        {
            if (oxygenValve == null) return;
            var temp = oxygenValve;


            oxygenValve = null;
        }
    }
}
