using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //public int playerNumber = 1;
    //public float movementSpeed = 3;
    public Vector2 currentPos = new Vector2();
    public GameObject handCrosshair;

    Animator anim;
    
    PlayerController playerController;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        currentPos = transform.position;

        Vector2 LDirection = new Vector2(Input.GetAxisRaw($"LHorizontal {playerController.playerNumber}"), Input.GetAxisRaw($"LVertical {playerController.playerNumber}"));
        Vector2 RDirection = new Vector2(Input.GetAxisRaw($"RHorizontal {playerController.playerNumber}"), Input.GetAxisRaw($"RVertical {playerController.playerNumber}"));
        RDirection.Normalize();

        float inputMagnitude = Mathf.Clamp01(LDirection.magnitude);
        LDirection.Normalize();

        transform.Translate(LDirection * playerController.moveSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);

        if (RDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, RDirection);
            handCrosshair.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
            
        }
        else if (LDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, LDirection);
            handCrosshair.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }


    }
}
