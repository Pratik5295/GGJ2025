using GGJ.Gameplay;
using UnityEngine;

public class FishTank : OxygenStation
{
    public override void SubmitOxygenValve(OxygenTank _valve)
    {
        oxygenValve = _valve;

        var go = oxygenValve.transform;

        go.SetParent(valveStoreLoc, false);
        go.localPosition = Vector3.zero;
        go.localRotation = Quaternion.identity;
        go.localScale = Vector3.one;

        //Make it ready for using the said oxygen tank
        SetOxygenTankForUse();
    }

    public void SetOxygenTankForUse()
    {
        oxygenValve.StartToUse();
    }
}
