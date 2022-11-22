using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public Vector2 currentPos = new Vector2();
    public float hAxis = 0;
    public float vAxis = 0;

    Rigidbody2D rbody;


    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        currentPos = rbody.position;
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(hAxis, vAxis);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
    }
}
