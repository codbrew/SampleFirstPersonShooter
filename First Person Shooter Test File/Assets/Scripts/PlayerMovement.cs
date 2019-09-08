using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public static GameObject player;
   
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    //we can alter to change the speed at which we move
    [SerializeField] private float moveSpeed;

    private float oldPos;

    //an animation curve that specifies the jump
    [SerializeField] private AnimationCurve jumpFallOff;
    //the force we will multiply to that animation curve
    [SerializeField] private float jumpMultiplyer;
    //the key that we will be using to jump
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private Animator anim;
    private bool currentlyWalking;
    private CharacterController charController;

    //is our character currently jumping?
    private bool isJumping;

    private void Awake()
    {
        //getting access to our character controller
        charController = GetComponent<CharacterController>();
        GetComponent<Animator>();
        oldPos = transform.position.x;

    }

    private void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        PlayerMove();
        MovementCheck();
      

    }

   private void MovementCheck()
    {
        if (oldPos != transform.position.x)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        oldPos = transform.position.x;
    }

    private void PlayerMove()
    {

        //this get out forward and back inputs plus side to side and multiply with move speed; SimpleMove applies Time.deltaTime
        float horizInput = Input.GetAxis(horizontalInputName) * moveSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * moveSpeed;
        
        //forward movement is equal to our forward motion times out veritcal input
        Vector3 forwardMovement = transform.forward * vertInput;
        //right movement is equal to side to side motion plus or horizontal input
        Vector3 rightMovement = transform.right * horizInput;

        //character controller has a simple movement function allowing us to pass in a forward and right movement together
        charController.SimpleMove(forwardMovement + rightMovement);
        

        jumpInput();
        currentlyWalking = true;
        
    }

    private void jumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplyer * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);
        charController.slopeLimit = 45.0f;
        isJumping = false;
        
    }
}
