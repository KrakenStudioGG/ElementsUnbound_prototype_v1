using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    public CastSpellEvent castSpellEvent;
    private Rigidbody2D myRB;
    private Camera mainCam;
    private Animator animator;
    private Vector2 MoveInput;
    private Vector2 MouseInput;
    private bool FireInput;
    private Vector3 mousePos;
    private readonly float playerSpeed = 0.65f;
    private static readonly int Walk = Animator.StringToHash("walk");

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerLook();
        FlipSprite();
        
        if (Input.GetMouseButtonDown(0))
        {
            CastSpell();
        }
    }

    public void OnMoveInput(Vector2 moveInput)
    {
        MoveInput = moveInput;
    }

    public void OnLookInput(Vector2 lookInput)
    {
        MouseInput = lookInput;
    }

    public void OnFireInput(bool fireInput)
    {
        FireInput = fireInput;
    }
    private void PlayerMove()
    {
        myRB.velocity = new Vector2(MoveInput.x * playerSpeed, MoveInput.y * playerSpeed);
    }

    private void PlayerLook()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mousePos = mainCam.ScreenToWorldPoint(MouseInput);
        mousePos.z = 0f;
    }

    private void FlipSprite()
    {
        var hasXVelocity = Mathf.Abs(myRB.velocity.x) > Mathf.Epsilon;
        var hasYVelocity = Mathf.Abs(myRB.velocity.y) > Mathf.Epsilon;
        if (hasXVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRB.velocity.x), 1f);
            animator.SetBool(id:Walk, true);
        }
        else if (hasYVelocity)
        {
            animator.SetBool(Walk, true);
        }
        else
        {
            animator.SetBool(Walk, false);
        }
    }

    private void CastSpell()
    {
        Debug.Log("FireInput");
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(Input.mousePosition));
        castSpellEvent.Invoke(transform.position, rayHit.point);
    }
}
