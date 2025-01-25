using System;
using UnityEngine;
using System.Collections.Generic;
using static GGJ.MetaConstants.EnumManager;
using System.Linq;
using Unity.VisualScripting;

namespace GGJ.Gameplay
{
    public class GenericStation : MonoBehaviour
    {
        [SerializeField]
        private StationState state;
        public StationState State => state;

        public Action<StationState> OnStationStateChangeEvent;

        [Header("Valves References")]

        [SerializeField]
        private List<GameValve> machineValves = new List<GameValve>();

        public int valveCount => machineValves.Count;

        [Space(10)]
        [Header("Breakage section")]

        [SerializeField]
        private float breakAfter;

        public bool IsBroken => state == StationState.BROKEN;

        [SerializeField]
        private int workingValves;


        private void RegisterValves()
        {
            //Register all events occurring in the valves
            if (machineValves.Count > 0)
            {
                foreach(GameValve valve in machineValves)
                {
                    valve.OnStateChangeEvent += OnValveStateChangeHandler;
                }
            }
        }

        private void UnregisterValves()
        {
            if (machineValves.Count > 0)
            {
                foreach (GameValve valve in machineValves)
                {
                    valve.OnStateChangeEvent -= OnValveStateChangeHandler;
                }
            }
        }

        private void SetState(StationState _state)
        {
            state = _state;
            OnStationStateChangeEvent?.Invoke(state);
        }

        private void Start()
        {
            RegisterValves();
        }

        private void OnDestroy()
        {
            UnregisterValves();
        }

        private void Update()
        {
            if (IsBroken)
            {
                Invoke("BreakMachine", breakAfter);
            }
        }

        private void BreakMachine()
        {
            //Randomly select a valve to break

            GameValve valveToBreak = machineValves.Single(valve => valve.State == ValveState.WORKING);

            valveToBreak.BreakValve();
        }


        private void OnValveStateChangeHandler(GameValve _valve, ValveState _state)
        {
            Debug.Log($"{_valve.name} has new state {_state.ToString()}");
        }
    }
}
