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
    public State currentState;

    public enum State
    {
        Dragging,
        Waiting,
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 LDirection = new Vector2(Input.GetAxisRaw($"LHorizontal {playerNumber}"), Input.GetAxisRaw($"LVertical {playerNumber}"));
        float inputMagnitude = Mathf.Clamp01(LDirection.magnitude);
        LDirection.Normalize();
        transform.Translate(LDirection * dragSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);

        if (Input.GetButtonDown("Submit") && currentState == State.Dragging)
        {
            DropObject();
        }

        if (Input.GetButtonDown("Submit") && colliding)
        {
            DragObject();
        }
    }
    private void DragObject()
    {
        draggedObject.transform.SetParent(gameObject.transform);

        currentState = State.Dragging;
    }

    private void DropObject()
    {
        Debug.Log("dropped");
        draggedObject.transform.SetParent(null);
        currentState = State.Waiting;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Draggable"))
        {
            colliding = true;
            draggedObject = other.gameObject;
        }

        if (other.gameObject.CompareTag($"PlayerZone"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Draggable"))
        {
            colliding = false;
        }
    }

   
}
