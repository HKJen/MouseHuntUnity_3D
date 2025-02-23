using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Parameters")]
    [Range(1,10)] [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jump = 2f;
    [SerializeField] private float gravity = 0.03f;
    [Space]
    [Range(1,3)] [SerializeField] private float rotationSpeed = 1.0f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [Tooltip("Set the top view angle of the camera")] [SerializeField] private float topClamp = 90.0f;
    [Tooltip("Set the bottom view angle of the camera")] [SerializeField] private float bottomClamp = -90.0f;

    [Header("GroundCheck")]
    [SerializeField] private float groundedOffset = 0.76f;
    [SerializeField] private float groundedRadius = 0.28f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Dialogue")]
    [SerializeField] private DialogDisplayer dialogDisplayer;
    
    [Header("Merchant")]
    [SerializeField] private MerchantDisplayer merchantDisplayer;
    
    private bool isGrounded;


    private float mouseX;
    private float mouseY;
    
    private float cinemachineTargetPitch;
    private float rotationVelocity;

    private CharacterController controller;

    private Vector3 inputDir;

    private PlayerAnimations anims;

    private float curSpeed = 5f;
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        anims = GetComponentInChildren<PlayerAnimations>();
        curSpeed = speed;
    }

    private void FixedUpdate()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        Gizmos.DrawSphere(spherePosition, groundedRadius);
    }

    
    void Update()
    {
        GetInput(); 
        DoRotation(); 
        CheckForJump(); 
        DoMovement();   
        DoAnimations();     
    }

    private void GetInput()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        inputDir = new Vector3(hor, inputDir.y, ver);

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed = runSpeed;
        }
        else
        {
            curSpeed = speed;
        }
    }

    private void DoRotation()
    {
        if (dialogDisplayer.IsInDialog || merchantDisplayer.IsWithMerchant)
            return;
            
        if(mouseX == 0f && mouseY == 0f)
            return;
        
        cinemachineTargetPitch += mouseY * rotationSpeed * -1;
        rotationVelocity = mouseX * rotationSpeed;

        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

        cameraTarget.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

        transform.Rotate(Vector3.up * rotationVelocity);
        
    }

    private void DoMovement()
    {
        Vector3 moveDirection = transform.TransformDirection(inputDir) * curSpeed;
        moveDirection.y = inputDir.y;

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void DoAnimations()
    {
        /*
        if(inputDir.x != 0 || inputDir.z != 0)
        {
            anims.SetWalkAnim(true);
        }
        else 
        {
            anims.SetWalkAnim(false);
        }
        */
        anims.SetMotionSpeed(inputDir.normalized.magnitude * curSpeed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anims.SetJumpAnim();
        }
        if(isGrounded)
        {
            //Debug.Log("Grounded");
            anims.SetFallAnim(false);
        }
        else
        {
            //Debug.Log("NOT_Grounded");
            anims.SetFallAnim(true);
        }

    }

    private void CheckForJump()
    {
        if(isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                inputDir.y = jump;
            }
            else
            {
                inputDir.y = 0f;
            }
        }
        else
        {
            inputDir.y -= gravity;
        }
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if( lfAngle < -360f) lfAngle += 360f;
        if( lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
