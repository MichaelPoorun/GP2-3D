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

}
