using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //public int playerNumber = 1;
    //public float movementSpeed = 3;
    public Vector2 currentPos = new Vector2();
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public GameObject handCrosshair;
    public SpriteRenderer handCrosshairSprite;
    public Rigidbody2D rBody2D;

    Animator anim;

    PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        handCrosshairSprite = handCrosshair.GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        currentPos = transform.position;

        //Vector2 LDirection = new Vector2(Input.GetAxisRaw($"LHorizontal {playerController.playerNumber}"), Input.GetAxisRaw($"LVertical {playerController.playerNumber}"));
        //Vector2 RDirection = new Vector2(Input.GetAxisRaw($"RHorizontal {playerController.playerNumber}"), Input.GetAxisRaw($"RVertical {playerController.playerNumber}"));
        lookDirection.Normalize();

        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
        moveDirection.Normalize();

        transform.Translate(moveDirection * playerController.moveSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);
        //rBody2D.velocity = LDirection * playerController.moveSpeed * inputMagnitude * Time.fixedDeltaTime;

        if (lookDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
            handCrosshair.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
        }
        else if (moveDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            handCrosshair.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (moveDirection.x < 0)
        {
            playerController.spriteRenderer.flipX = true;
            handCrosshairSprite.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            playerController.spriteRenderer.flipX = false;
            handCrosshairSprite.flipX = false;
        }
    }

    void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }

    void OnLook(InputValue input)
    {
        lookDirection = input.Get<Vector2>();
    }
}
