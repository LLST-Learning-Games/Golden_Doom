using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private GameObject playerBody = null;
    [SerializeField] private Rigidbody myRb = null;
    private Vector3 leftFacingRotation = new Vector3(0, 180f, 0);
    private Vector3 rightFacingRotation = new Vector3();
    [SerializeField] private Vector3 movementVector;
    private Vector2 inputMovementVector;
    [SerializeField] private bool isJumping = false;

    // Start is called before the first frame update
    private void Start()
    {
        if(myRb == null)
            myRb = GetComponent<Rigidbody>();
        
        movementVector = new Vector3();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //movementVector = new Vector3();
        //movementVector.y = myRb.velocity.y;

        //HandleMoveInput();
        //HandleJumpInput();
        ApplyMoveVector();

        if (isJumping && CheckGrounded() && myRb.velocity.y < 0)
            isJumping = false;
    }

    private void OnMove(InputValue movementValue)
    {
        inputMovementVector = movementValue.Get<Vector2>();

    }

    private void OnJump()
    {
        if (!isJumping && CheckGrounded())
        {
            myRb.AddForce(0, jumpForce, 0);
            isJumping = true;
        }
    }

    private void ApplyMoveVector()
    {
        if (movementVector.x < 0)
            playerBody.transform.eulerAngles = leftFacingRotation;
        if (movementVector.x > 0)
            playerBody.transform.eulerAngles = rightFacingRotation;

        movementVector = new Vector3(inputMovementVector.x * moveSpeed,
            myRb.velocity.y,
            inputMovementVector.y * moveSpeed);
        myRb.velocity = movementVector;
    }

    // 
    // Note: this approach to handling jumping has been replaced
    // by the Input System in the GUI. See OnJump().
    //
    //private void HandleJumpInput()
    //{
    //    if (!isJumping && Keyboard.current.spaceKey.isPressed && CheckGrounded())
    //    {
    //        movementVector.y = jumpForce;
    //        isJumping = true;
    //    }

    //    if (CheckGrounded() && !Keyboard.current.spaceKey.isPressed)
    //    {
    //        isJumping = false;
    //    }
    //}

    // 
    // Note: this approach to handling movement has been replaced
    // by the Input System in the GUI. See OnMove().
    //
    //private void HandleMoveInput()
    //{
    //    if (Keyboard.current.wKey.isPressed)
    //    {
    //        movementVector.z = moveSpeed;
    //    }
    //    if (Keyboard.current.sKey.isPressed)
    //    {
    //        movementVector.z = -moveSpeed;
    //    }
    //    if (Keyboard.current.aKey.isPressed)
    //    {
    //        movementVector.x = -moveSpeed;
    //    }
    //    if (Keyboard.current.dKey.isPressed)
    //    {
    //        movementVector.x = moveSpeed;
    //    }
    //}



    private bool CheckGrounded()
    {
        Vector3 checkPosition = transform.position;
        checkPosition.y = transform.position.y + .1f;
        float checkDistance = 0.2f;
        return Physics.Raycast(checkPosition, Vector3.down, checkDistance);
    }


}
