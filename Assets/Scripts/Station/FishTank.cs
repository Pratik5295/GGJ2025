using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGJ.Gameplay;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

public class FishTank : MonoBehaviour
{
    [Header("Valves References")]

    [SerializeField]
    private List<FishTankOxyStation> machineValves = new List<FishTankOxyStation>();

    public int valveCount => machineValves.Count;

    public bool IsBroken = false;


    [SerializeField]
    private Transform valvesHolder;
    [SerializeField]
    private float timerCounter = 0f;

    [SerializeField]
    private float maxBreakTimeAllowed;

    public bool AreAllValvesFull() => machineValves.All(valve => valve.OxygenStationState != OxyStatonState.NEEDOXY);


    [ContextMenu("Find All Valves")]
    public void FindValves()
    {
        machineValves.Clear();
        for (int i = 0; i < valvesHolder.childCount; i++)
        {
            var child = valvesHolder.GetChild(i);
            machineValves.Add(child.GetComponent<FishTankOxyStation>());
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.IsGameOver) return;

        if (ScreenManager.Instance.ActiveKey != ScreenManager.ScreenKey.GAME) return;

        if (!AreAllValvesFull())
        {
            IsBroken = true;
        }
        else
        {
            IsBroken = false;
            ResetLoseConditionCounter();
        }

        if (IsBroken)
        {
            CheckIfBroken();
        }
    }


    /// <summary>
    /// The lose condition determiner will start if broken is set ot true
    /// </summary>
    private void CheckIfBroken()
    {
        timerCounter += Time.deltaTime;

        if (timerCounter > maxBreakTimeAllowed)
        {
            Debug.Log("Game over!");

            GameManager.Instance.SetGameOver();
        }
    }

    private void ResetLoseConditionCounter()
    {
        timerCounter = 0f;
    }


}
