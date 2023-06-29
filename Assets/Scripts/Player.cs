using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    public static Player Instance;

    GameObject character;
    Animator chaAnimator;
    PlayerControls input;
    // Start is called before the first frame update

    Rigidbody rigid;

    HealthSystem healthSystem;

    public Vector3 moveVector;
    Vector2 inputValue;
    public float speed= 10f;

    public GameObject[] spawnPoints;
    bool isRun;
    void Awake()
    {
        Instance = this;

        input = new PlayerControls();
        input.Player.Move.Enable();
        input.Player.Move.performed += PlayerMove;
        input.Player.Move.canceled += PlayerMove;

        character = transform.Find("Character").gameObject;
        chaAnimator = character.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        healthSystem = GetComponent<HealthSystem>();

    }
    void Start()
    {
        
    }

  
    float rotationSpeed = 50f;
    // Update is called once per frame
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
    }

    private void FixedUpdate() 
    {
        rigid.velocity = moveVector * speed * Time.fixedDeltaTime;

        if (moveVector != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVector);
        }
        
    }

    void PlayerMove(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<Vector2>().normalized;
    }

}
