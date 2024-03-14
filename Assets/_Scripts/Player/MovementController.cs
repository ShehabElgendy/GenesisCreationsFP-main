using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField]
    private float lookSpeed = 2f;

    [SerializeField]
    private float lookXLimit = 45f;

    [SerializeField]
    private Camera playerCamera;

    private Vector3 moveDirection = Vector3.zero;

    private float rotationX = 0;

    private bool canMove = true;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(canMove)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = moveSpeed * Input.GetAxis(GameStatics.VerticalMovement);
            float curSpeedY = moveSpeed * Input.GetAxis(GameStatics.HorizontalMovement);
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        characterController.SimpleMove(moveDirection * Time.deltaTime);
       // if (canMove)
        {
            rotationX += -Input.GetAxis(GameStatics.Mouse_Y) * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis(GameStatics.Mouse_X) * lookSpeed, 0);
        }
    }

    public void MovementAbility(bool _canMove)
    {
        canMove = _canMove;
        characterController.enabled = canMove;
    }
}