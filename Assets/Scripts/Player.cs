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


    public Vector3 direction;
    public float speed= 10f;

    public GameObject[] spawnPoints;

    void Awake()
    {
        Instance = this;

        input = new PlayerControls();
        input.Player.Move.Enable();
        input.Player.Move.performed += PlayerMove;
        input.Player.Move.canceled += PlayerMove;

        character = transform.Find("Character").gameObject;
        chaAnimator = character.GetComponent<Animator>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed, Space.World);

        if (direction.magnitude > 0) 
        {
            chaAnimator.SetBool("Run", true);
        }
        else{
            chaAnimator.SetBool("Run", false);
        }

        
    }

    void PlayerMove(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>().normalized;
        direction = new Vector3(inputValue.x, 0, inputValue.y);

        if(direction != Vector3.zero)
        {
            character.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
