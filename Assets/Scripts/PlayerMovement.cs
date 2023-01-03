using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 currentPos = new Vector2();
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public GameObject handCrosshair;
    public SpriteRenderer handCrosshairSprite;
    public Rigidbody2D rBody2D;
    public SpriteRenderer PlayerRenderer;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioSource audioSource;

    public GameObject smokePrefab;
    public Transform smokePoint;

    Animator anim;

    PlayerController playerController;
    private List<Collider2D> playerColliders;
    private List<Collider2D> myColliders;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        handCrosshairSprite = handCrosshair.GetComponentInChildren<SpriteRenderer>();
        PlayerRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rBody2D = GetComponent<Rigidbody2D>();
        rBody2D.gravityScale = 0f;
        myColliders = new List<Collider2D>();
        myColliders.AddRange(GetComponentsInChildren<Collider2D>());

        FindOtherPlayersCollidersForDashAbility();
    }

    private void FindOtherPlayersCollidersForDashAbility()
    {
        var playerCharacters = FindObjectsOfType<PlayerMovement>();
        playerColliders = new List<Collider2D>();

        foreach (var player in playerCharacters)
        {
            playerColliders.AddRange(player.GetComponentsInChildren<Collider2D>());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
            return;

        currentPos = transform.position;
        lookDirection.Normalize();

        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
        moveDirection.Normalize();
        transform.Translate(moveDirection * playerController.moveSpeed * inputMagnitude * Time.fixedDeltaTime, Space.World);
        
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

        if (playerController.isBlocking == true)
            handCrosshairSprite.gameObject.SetActive(false);
        else if (playerController.isBlocking == false)
            handCrosshairSprite.gameObject.SetActive(true);
    }

    void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }

    void OnLook(InputValue input)
    {
        lookDirection = input.Get<Vector2>();
    }

    void OnDash()
    {
        if (enabled && canDash)
            StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        PlayerPhysicsBypass(true);

        if (playerController.isBlocking == false)
        {
            Quaternion inverseToRotation = Quaternion.LookRotation(Vector3.forward, -moveDirection);
            canDash = false;
            isDashing = true;
            playerController.dashing = true;

            //if (moveDirection.x < 0 && moveDirection.y > 0)
            //{
            //    GameObject newSmoke = Instantiate(smokePrefab, smokePoint.position, smokePoint.rotation);
            //    newSmoke.GetComponent<SpriteRenderer>().flipX = false;
            //    Destroy(newSmoke, 0.3f);
            //}
            //if (moveDirection.x > 0)
            //{
            //    GameObject newSmoke = Instantiate(smokePrefab, smokePoint.position, smokePoint.rotation);
            //    newSmoke.GetComponent<SpriteRenderer>().flipX = true;
            //    Destroy(newSmoke, 0.3f);
            //}

            audioSource.PlayOneShot(dashSound);
            GameObject newSmoke = Instantiate(smokePrefab, smokePoint.position - (Vector3)moveDirection.normalized, inverseToRotation);
            Destroy(newSmoke, 0.3f);

            rBody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * dashingPower;
            yield return new WaitForSeconds(dashingTime);

            rBody2D.velocity = new Vector2(0f, 0f);
            playerController.dashing = false;
            isDashing = false;
            PlayerPhysicsBypass(false);
            yield return new WaitForSeconds(dashingCooldown);

            canDash = true;
        }
    }

    private void PlayerPhysicsBypass(bool ignore)
    {
        foreach (var item in playerColliders)
        {
            foreach (var collider in myColliders)
            {
                Physics2D.IgnoreCollision(item, collider, ignore);
            }
        }
    }
}
