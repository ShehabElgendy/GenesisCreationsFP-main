using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{    public void Interact(Interactor _interactor, out bool _interactionSuccefull);
}
