using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GGJ.MetaConstants.EnumManager;

namespace GGJ.Gameplay
{
    public class OxygenRefillingStation : MonoBehaviour
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

        [SerializeField]
        private float breakMax;

        [Tooltip("Minimum after which the machine will definitely break")]
        [SerializeField]
        private float breakThreshold;


        public bool IsBroken = false;

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
            IsBroken = true;
        }

        private void Start()
        {
            RegisterValves();

            DetermineNextBreakAfter();
        }

        private void OnDestroy()
        {
            UnregisterValves();
        }

        private void Update()
        {
            if (ScreenManager.Instance.ActiveKey != ScreenManager.ScreenKey.GAME) return;

            if (!IsBroken)
            {
                IsBroken = true;
                Invoke("BreakMachine", breakAfter);

                //Determine the next break value
                DetermineNextBreakAfter();
            }
        }

        private void DetermineNextBreakAfter()
        {
            breakAfter = UnityEngine.Random.Range(breakThreshold, breakMax);
        }

        private void BreakMachine()
        {
            //Randomly select a valve to break
            int index = (int)UnityEngine.Random.Range(0,valveCount);

            GameValve valveToBreak = machineValves[index];

            valveToBreak.BreakValve();

            SetState(StationState.BROKEN);
        }


        public bool AreAllValvesWorking() => machineValves.All(valve => valve.State == ValveState.WORKING);


        private void OnValveStateChangeHandler(GameValve _valve, ValveState _state)
        {
            Debug.Log($"{_valve.name} has new state {_state.ToString()}");

            if(!AreAllValvesWorking())
            {
                SetState(StationState.BROKEN);
            }
            else
            {
                //Machine is working 
                SetState(StationState.WORKING);
                IsBroken = false;
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
