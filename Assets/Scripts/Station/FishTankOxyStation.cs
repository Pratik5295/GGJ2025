using System.Collections.Generic;
using System.Linq;
using GGJ.Gameplay;
using GGJ.Managers;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

public class FishTankOxyStation : OxygenStation
{
    [SerializeField]
    private OxyStatonState oxyStatonState;

    public OxyStatonState OxygenStationState => oxyStatonState;

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

        oxyStatonState = OxyStatonState.FULL;

        AudioManager.Instance.PlayForegroundSound(1);
    }

    public void SetOxygenTankForUse()
    {
        oxygenValve.StartToUse();
    }

    private void Update()
    {
        if(oxygenValve == null)
        {
            oxyStatonState = OxyStatonState.NEEDOXY;
        }
        else
        {
            //Check if the valve is empty
            if(oxygenValve.TankState == OxyState.EMPTY)
            {
                oxyStatonState = OxyStatonState.NEEDOXY;
            }
        }
    }

}
