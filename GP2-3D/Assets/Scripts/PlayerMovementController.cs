using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController controller;

    [Header("Adjustable Variables")]
    [SerializeField] public float horizontalAcceleration;

    [SerializeField] public float horizontalMaxSpeed;

    [SerializeField] public float verticalAccelleration;

    [SerializeField] public float verticalMaxSpeed;

    [SerializeField] public float gravityAccel;

    [SerializeField] public float maxFallSpeed;

    [SerializeField] public float friction;

    Vector3 currentVelocity;

    bool canMove = true;

    //public PlayerStates State = PlayerStates.Idle;

    //PlayerStates st = PlayerStates.Idle;

    //if walking under press left or right st = PlayerStates.Walking;

    //if(!CanJump()) Goes outside of all functions and voids and stuff; ensures youre always checking if you can jump and should it be playing a jump animation
    //{
    //  st.PlayerStates.Jumping;
    //}
    //State = st;
    //if (State == PlayerStates.Idle)
    //{
    //  SR.color.white;
    //}
    //else if(State == PlayerStates.Walking)
    //{
    //  sr.color.yellow;
    //}


    private void Update()
    {
        PhysicsUpdate();

        Vector3 playerInput = Vector3.zero;
        if (canMove)
        {
            Transform camTransform = Camera.main.transform;
            Vector3 verticalInput = new Vector3(0f, Input.GetAxis("Jump"), 0f);
            Vector3 horizontalInput = camTransform.right * Input.GetAxis("Horizontal") + camTransform.forward * Input.GetAxis("Vertical");

            playerInput = verticalInput + horizontalInput;

            AddVelocity(horizontalInput.normalized * horizontalAcceleration, horizontalMaxSpeed);
            AddVelocity(verticalInput.normalized * verticalAccelleration, verticalMaxSpeed);
        }

        controller.Move(currentVelocity * Time.deltaTime);
    }

    public void SetMove(bool value)
    {
        canMove = value;
    }

    public void AddVelocity(Vector3 velocity, float speedLimit)
    {
        if (Vector3.Dot(currentVelocity, velocity.normalized) < speedLimit)
        {
            currentVelocity += velocity;
        }
    }

    private void PhysicsUpdate()
    {
        if (currentVelocity.magnitude > 0)
        {
            Vector3 forceOfFriction = (-currentVelocity.normalized) * friction;

            if (currentVelocity.magnitude > forceOfFriction.magnitude)
            {
                currentVelocity += forceOfFriction;
            }

            else
            {
                currentVelocity = Vector3.zero;
            }
        }

        AddVelocity(Vector3.down * gravityAccel, maxFallSpeed); // Gravity
    }

    //public enum PlayerStates
    //{
    //    None = 0,
    //    Idle = 1,
    //    Walking = 2,
    //    Jumping = 3,
    //    Stunned = 4
    //}

    //public void SetState(PlayerStates st)
   // {
       // State = st;
       // if(State == PlayerStates.Idle)            use this to change states
   //}

    //if (State == PlayerStates.Stunned)
    //{
    //  StunControls(); within this void you can make stunnedtimer decrease over time via time.deltatTime and then set the state to idle afterwards so that the player can move again
    //  return;
    //}
    //else
    //{
    //  NormalControls(); 
    //}

}
