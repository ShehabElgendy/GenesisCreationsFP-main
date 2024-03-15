using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Transform InteractionPoint;

    [SerializeField]
    public LayerMask InteractionLayer;

    [SerializeField]
    public float InteractionPointRadius = 1f;

    public bool isInteracting { get; private set; }

    private void Update()
    {
        var _colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                var _interactables = _colliders[i].GetComponent<IInteractable>();

                if (_interactables != null)
                {
                    StartInteraction(_interactables);
                }
            }
        }
    }

    private void StartInteraction(IInteractable _interactables)
    {
        _interactables.Interact(this, out bool _interactSuccessful);
        isInteracting = true;
    }

    private void EndInteraction()
    {
        isInteracting = false;
    }
}
