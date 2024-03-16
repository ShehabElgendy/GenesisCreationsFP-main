using UnityEngine;
using UnityEngine.Events;

public class BedInteraction : MonoBehaviour, IInteractable
{
    public static UnityAction<PlayerController> OnBedInteractionRequested { get; set; }
    public void Interact(Interactor _interactor, out bool _interactionSuccefull)
    {
        var _player = _interactor.GetComponent<PlayerController>();

        if (_player != null)
        {
            OnBedInteractionRequested?.Invoke(_player);
            _interactionSuccefull = true;
        }
        else
        {
            _interactionSuccefull = false;
        }
    }
}
