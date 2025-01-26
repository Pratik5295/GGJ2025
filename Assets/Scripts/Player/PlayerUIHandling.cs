using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(3)]
public class PlayerUIHandling : MonoBehaviour
{
    [SerializeField]
    private StarterAssetsInputs input;

    private void Start()
    {
        if(input != null)
        {
            input.OnNextEvent += OnNextEventHandler;
        }
    }

    private void OnDestroy()
    {
        if (input != null)
        {
            input.OnNextEvent -= OnNextEventHandler;
        }

    }

    private void OnNextEventHandler(bool _next)
    {
        if (_next)
        {
            input.next = false;

            //Play next UI frame?
        }
    }
}
