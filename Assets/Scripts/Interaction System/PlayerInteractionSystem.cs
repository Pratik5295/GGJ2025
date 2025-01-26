using GGJ.Gameplay.Interfaces;
using GGJ.Gameplay.Player;
using GGJ.Toaster;
using UnityEngine;
using UnityEngine.UIElements;

namespace GGJ.Gameplay.System
{
    /// <summary>
    /// Interaction System player handler.
    /// This class will have reference to current interactable object detected
    /// </summary>
    /// 

    [DefaultExecutionOrder(1)]
    public class PlayerInteractionSystem : MonoBehaviour
    {
        [SerializeField]
        private BaseInteractable detectableObject;

        //Held Object
        [SerializeField]
        private BaseInteractable currentInteractableObject;

        public BaseInteractable CurrentInteractableObject => currentInteractableObject;

        [SerializeField]
        private Transform heldObjectTransform;

        public bool HasDetectableObject => detectableObject != null;

        [SerializeField]
        private Transform playerCapsule;


        public void DetectInteractableObject(BaseInteractable interactableObject)
        {
            if(currentInteractableObject != null)
            {
                Debug.Log("Left area, fire force reset");

                if (!currentInteractableObject.GetComponent<BasePickable>())
                {
                    ResetInteraction();
                }

            }

            detectableObject = interactableObject;
        }

        public void HandlePlayerInteraction()
        {
            if (detectableObject == null && currentInteractableObject == null) return;

            if (currentInteractableObject != null)
            {
                if (IsHoldingOxygenCylinder())
                {
                    //Submit the cylinder to station
                    if(PlayerManager.Instance.CurrentOxygenStation != null)
                    {
                        var heldCylinder = GetHeldOxygenCylinder();
                        if (!PlayerManager.Instance.CurrentOxygenStation.HasCylinder)
                        {
                            //Check if the main machine is broken
                            if (!IsMachineBroken(PlayerManager.Instance.CurrentOxygenStation))
                            {
                                PlayerManager.Instance.CurrentOxygenStation.SubmitOxygenValve(heldCylinder);

                                //Place the item
                                currentInteractableObject.ResetInteract();

                                currentInteractableObject = null;
                            }
                            else
                            {
                                Debug.Log("Machine is broken, cant add anything. Fix it");
                                ToasterManager.Instance.PopulateToasterMessage("Supply machine is broken, unable to refill oxygen");
                            }
                        }
                        else
                        {
                            //Already has a cylinder, just return

                            return;
                        }
                    }
                    else
                    {
                        //No oxygen station was found, Just drop it (Throw it)

                        if (currentInteractableObject.TryGetComponent<BasePickable>(out var heldItem))
                        {
                            //Already carrying an object, drop it
                            currentInteractableObject.GetComponent<BasePickable>().Drop();

                            var heldItemGO = currentInteractableObject.gameObject;
                            heldItemGO.transform.SetParent(null);

                            heldItemGO.transform.localRotation = Quaternion.identity;
                            heldItemGO.transform.localScale = Vector3.one;

                            Vector3 throwForce = playerCapsule.forward * 10f;
                            heldItem.GetComponent<Rigidbody>().AddForce(throwForce, ForceMode.Impulse);  
                        }

                        currentInteractableObject = null;
                    }
                }
                else
                {
                    DropHeldItem();
                }

            }
            else
            {
                //Pick item
                if(detectableObject.TryGetComponent<BasePickable>(out var pickable))
                {

                    //Do separate check for oxygen tank
                    if (PlayerManager.Instance.CurrentOxygenStation != null)
                    {
                        var oxyStation = PlayerManager.Instance.CurrentOxygenStation;
                        if (oxyStation.HasCylinder)
                        {
                            //Picking up held oxygen tank

                            oxyStation.TakeValve();
                        }
                    }

                    currentInteractableObject = pickable;

                    var heldItemGO = currentInteractableObject.gameObject;
                    heldItemGO.transform.SetParent(heldObjectTransform);

                    heldItemGO.transform.localPosition = Vector3.zero;
                    heldItemGO.transform.localRotation = Quaternion.identity;
                    heldItemGO.transform.localScale = Vector3.one;


                    //Notify item that it is picked
                    pickable.Pick();
                }
                else
                {
                    Debug.LogWarning("This item cannot be picked, but only interacted with");

                    //Check it is a valve
                    if(detectableObject.TryGetComponent<BaseInteractable>(out var gameValve))
                    {
                        currentInteractableObject = gameValve;

                        currentInteractableObject.Interact();
                    }

                }
            }
        }

        public void ResetInteraction()
        {
            //Check if it was holding or interacting with an item

            if(currentInteractableObject == null) return;

            if (currentInteractableObject.GetComponent<BasePickable>()) return;

            //Player was holding an item
            currentInteractableObject.ResetInteract();

            currentInteractableObject = null;

        }

        public void ForcePlayerPick(BasePickable _pick)
        {
            currentInteractableObject = _pick;

            var heldItemGO = currentInteractableObject.gameObject;
            heldItemGO.transform.SetParent(heldObjectTransform);


            //Notify item that it is picked
            _pick.Pick();
        }    

        private bool IsHoldingOxygenCylinder()
        {
            return currentInteractableObject.TryGetComponent<OxygenTank>(out var heldItem);
            
        }

        private OxygenTank GetHeldOxygenCylinder()
        {
            currentInteractableObject.TryGetComponent<OxygenTank>(out var heldItem);

            return heldItem;
        }

        private void DropHeldItem()
        {
            if (currentInteractableObject.TryGetComponent<BasePickable>(out var heldItem))
            {
                //Already carrying an object, drop it
                currentInteractableObject.GetComponent<BasePickable>().Drop();

                var heldItemGO = currentInteractableObject.gameObject;
                heldItemGO.transform.SetParent(null);

                heldItemGO.transform.localRotation = Quaternion.identity;
                heldItemGO.transform.localScale = Vector3.one;
            }

            currentInteractableObject = null;
        }

        private bool IsMachineBroken(OxygenStation _station)
        {
            return !_station.IsMainMachineWorking;
        }

        
    }
}
