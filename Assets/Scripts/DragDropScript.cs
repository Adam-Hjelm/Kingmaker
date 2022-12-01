using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScript : MonoBehaviour
{
    public int playerNumber;
    public float dragSpeed;
    public bool colliding = false;

    public GameObject draggedObject;
    //public Collider2D draggedCollider;
    public State currentState;

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

        if (Input.GetButtonDown($"Submit{playerNumber}") && currentState == State.Dragging)
        {
            Debug.Log("pressed submit");
            DropObject();
        }

        if (Input.GetButtonDown($"Submit{playerNumber}") && colliding)
        {
            DragObject();
        }
    }

    private void MoveDragNDropCursor()
    {
        Vector2 LDirection = new Vector2(Input.GetAxisRaw($"LHorizontal {playerNumber}"), Input.GetAxisRaw($"LVertical {playerNumber}"));
        float inputMagnitude = Mathf.Clamp01(LDirection.magnitude);
        LDirection.Normalize();
        transform.Translate(LDirection * dragSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);
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
