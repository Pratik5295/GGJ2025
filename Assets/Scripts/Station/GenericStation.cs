using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

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

        [SerializeField]
        private Transform valvesHolder;


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
            if (!IsBroken)
            {
                Invoke("BreakMachine", breakAfter);
            }
        }

        private void BreakMachine()
        {
            //Randomly select a valve to break

            GameValve valveToBreak = machineValves.Single(valve => valve.State == ValveState.WORKING);

            valveToBreak.BreakValve();

            SetState(StationState.BROKEN);
        }

        public bool IsMachineBroken() => machineValves.Any(valve => valve.State == ValveState.BROKEN);


        private void OnValveStateChangeHandler(GameValve _valve, ValveState _state)
        {
            Debug.Log($"{_valve.name} has new state {_state.ToString()}");

            if(_state == ValveState.WORKING)
            {
                //The valve is working again
            }
        }

        [ContextMenu("Find All Valves")]
        public void FindValves()
        {
            machineValves.Clear();
            for (int i = 0; i < valvesHolder.childCount; i++)
            {
                var child = valvesHolder.GetChild(i);
                machineValves.Add(child.GetComponent<GameValve>());
            }
        }
    }
}
