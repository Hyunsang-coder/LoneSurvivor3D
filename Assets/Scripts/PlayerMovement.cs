using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    GameObject character;
    Animator chaAnimator;
    PlayerControls input;
    // Start is called before the first frame update

    CharacterController controller;

    HealthSystem healthSystem;

    public Vector3 moveVector;
    Vector2 inputValue;
    public float speed= 10f;

    public bool isGrounded;
    const float gravity = -9.81f;
    public float rayLength = 0.2f;
    public GameObject[] spawnPoints;
    bool isRun;

    public int level= 1;


    void Awake()
    {
        Instance = this;

        input = new PlayerControls();
        input.Player.Move.Enable();
        input.Player.Move.performed += PlayerMove;
        input.Player.Move.canceled += PlayerMove;

        character = transform.Find("Character").gameObject;
        chaAnimator = character.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>();

    }
    void Start()
    {
        level = 1;
    }


    void Update()
    {
        moveVector = new Vector3(inputValue.x, 0, inputValue.y);

        if (moveVector.magnitude > 0 && !isRun) 
        {
            chaAnimator.SetBool("Run", true);
            isRun = true;
        }
        else if (moveVector.magnitude == 0)
        {
            chaAnimator.SetBool("Run", false);
            isRun = false;
        }

        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, -transform.up, out hit, rayLength))
        {
            isGrounded = true;
            moveVector.y = 0;
        }
        else
        {
            isGrounded = false;
            moveVector.y += gravity;

        }
        Debug.DrawRay(transform.position, -transform.up *rayLength, Color.red);

    }

    private void FixedUpdate() 
    {
        
        controller.Move(moveVector * Time.fixedDeltaTime * speed);
        
        

        if (moveVector != Vector3.zero)
        {   
            Vector3 direction = new Vector3(moveVector.x, 0, moveVector.z);
            transform.LookAt(transform.position + direction);
        }

    }

    void PlayerMove(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<Vector2>().normalized;
    }

    public int PlayerLevelUp()
    {
        level ++;
        return level;
    }

}
