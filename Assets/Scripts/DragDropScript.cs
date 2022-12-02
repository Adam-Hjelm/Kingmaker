using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropScript : MonoBehaviour
{
    public int playerNumber;
    public float dragSpeed;
    public bool colliding = false;

    public GameObject draggedObject;
    //public Collider2D draggedCollider;
    public State currentState;

    public PlayerInput pInput;
    [SerializeField] Vector2 moveDirection;


    public enum State
    {
        Dragging,
        Waiting,
    }

    void Start()
    {
        currentState = State.Waiting;
    }

    void Update()
    {
        MoveDragNDropCursor();
    }

    public void OnFire()
    {
        if (currentState == State.Dragging)
        {
            Debug.Log("pressed submit");
            DropObject();
        }

        if (colliding)
        {
            DragObject();
        }
    }

    public void OnMove(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void MoveDragNDropCursor()
    {
        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
        //LDirection.Normalize();
        transform.Translate(moveDirection.normalized * dragSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);
    }

    private void DragObject()
    {
        draggedObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        draggedObject.transform.SetParent(gameObject.transform);
        currentState = State.Dragging;
    }

    private void DropObject()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        draggedObject.GetComponent<Collider2D>().enabled = true;
        draggedObject.transform.SetParent(null);

        colliding = false;
        currentState = State.Waiting;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"DraggablePlayer{playerNumber}"))
        {
            colliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"DraggablePlayer{playerNumber}"))
        {
            colliding = true;
            draggedObject = other.gameObject;
        }
    }
}
