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
        Vector2 LDirection = new Vector2(Input.GetAxisRaw($"LHorizontal {playerNumber}"), Input.GetAxisRaw($"LVertical {playerNumber}"));
        float inputMagnitude = Mathf.Clamp01(LDirection.magnitude);
        LDirection.Normalize();
        transform.Translate(LDirection * dragSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);

        if (Input.GetButtonDown($"Submit{playerNumber}") && currentState == State.Dragging)
        {
            DropObject();
        }

        if (Input.GetButtonDown($"Submit{playerNumber}") && colliding)
        {
            DragObject();
        }
    }
    private void DragObject()
    {
        draggedObject.transform.SetParent(gameObject.transform);
        draggedObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        currentState = State.Dragging;
    }

    private void DropObject()
    {
        draggedObject.transform.SetParent(null);
        colliding = false;
        currentState = State.Waiting;
        gameObject.GetComponent<Collider2D>().enabled = true;
        draggedObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"DraggablePlayer{playerNumber}"))
        {
            colliding = true;
            draggedObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"DraggablePlayer{playerNumber}"))
        {
            colliding = false;
        }
    }
}
