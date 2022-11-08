using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public delegate void EventHandler(Vector2 playerPos, Vector2 rayPos, int index);
    public static event EventHandler castEvent;
    
    private Rigidbody2D myRB;
    private PlayerInput inputComponent;
    private bool isPlayerInputInitialized;
    private Camera mainCam;
    private Animator animator;
    private Vector2 MoveInput;
    private Vector2 MouseInput;
    private float FireInput;
    private Vector3 mousePos;
    private readonly float playerSpeed = 0.65f;
    private static readonly int Walk = Animator.StringToHash("walk");

    private void InitPlayerInputs()
    {
        if (!inputComponent.isActiveAndEnabled) return;
        inputComponent.actions["Move"].performed += OnMoveInput;
        inputComponent.actions["Move"].canceled += OnMoveInput;
        inputComponent.actions["Look"].performed += OnLookInput;
        inputComponent.actions["Look"].canceled += OnLookInput;
        inputComponent.actions["Fire"].started += OnFireInput;
        inputComponent.actions["Fire"].performed += OnFireInput;
        inputComponent.actions["Fire"].canceled += OnFireInput;
        isPlayerInputInitialized = true;
    }

    private void OnDisable()
    {
        inputComponent.actions["Move"].performed -= OnMoveInput;
        inputComponent.actions["Look"].performed -= OnLookInput;
        inputComponent.actions["Fire"].started -= OnFireInput;
        inputComponent.actions["Fire"].performed -= OnFireInput;
        inputComponent.actions["Fire"].canceled -= OnFireInput;
        isPlayerInputInitialized = false;
    }

    private void OnMoveInput(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnLookInput(InputAction.CallbackContext ctx)
    {
        MouseInput = ctx.ReadValue<Vector2>();
    }

    private void OnFireInput(InputAction.CallbackContext ctx)
    {
        FireInput = ctx.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputComponent = GetComponent<PlayerInput>();
        myRB = GetComponent<Rigidbody2D>();
        
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isPlayerInputInitialized) InitPlayerInputs();
        PlayerMove();
        PlayerLook();
        FlipSprite();
        
        if (Input.GetMouseButtonDown(0))
        {
            CastSpell();
        }
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

    private void CastSpell()
    {
        Vector2 currentPos = transform.position;
        Debug.Log("FireInput");
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(Input.mousePosition));
        castEvent?.Invoke(currentPos, rayHit.point, 0);
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
}
