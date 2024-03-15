using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ATMInteraction : MonoBehaviour, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public static UnityAction<PlayerController> OnATMWindowRequested { get; set; }

    public void Interact(Interactor _interactor, out bool _interactionSuccefull)
    {
        var _player = _interactor.GetComponent<PlayerController>();

        if (_player != null)
        {
            OnATMWindowRequested?.Invoke(_player);
            _interactionSuccefull = true;
        }
        else
        {
            _interactionSuccefull = false;
        }
    }
}
