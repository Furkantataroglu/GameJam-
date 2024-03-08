using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header ("Walking")]
    [SerializeField] private float speed; //walking speed
    
    [SerializeField] private float currentSpeed; 
    [SerializeField] private float accelerationLerp; 
    [SerializeField] private float decelerationLerp; 
    [Header ("jumping")]
    [SerializeField]private float jumpForce; 
    [SerializeField]private float jumpingGravityScale;
    [SerializeField]private float normalGravityScale; 
        [SerializeField]private float coyotiTime =.1f;   //cayotiTime
    private float lastGroundedTime =-10f; 
    [SerializeField]private float jumpBufferDuration = .1f;
    private float lastJumpTryTime =-10f; 
    [Header ("GroundCheck")]
    [SerializeField]private Transform groundCheck;  
    [SerializeField]private float groundCheckRadius;
    [SerializeField]private LayerMask groundLayers;
    private bool isGrounded;   
    
    private Rigidbody2D rb; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {   
     ManageWalking();
    }
    private void FixedUpdate() 
    {
      
    }

    private void ManageWalking()
    {
        var xinput = Input.GetAxisRaw("Horizontal"); 
        var yinput = Input.GetAxisRaw("Vertical");
        
       
        
       //makes speed 0 if no input
        if(xinput == 0 && yinput == 0 )
        {
             currentSpeed = Mathf.Lerp(currentSpeed , 0f, accelerationLerp * Time.deltaTime);
        }
        
        else if(rb.velocity.x > 0 && xinput > 0 || rb.velocity.x < 0 && xinput < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed , speed, accelerationLerp * Time.deltaTime); 
                                       
        }
        else    //şimdi yavaşlama hızı ile 
        {
            currentSpeed = Mathf.Lerp(currentSpeed , speed, decelerationLerp * Time.deltaTime);
        }
        
        rb.velocity = new Vector2(xinput * currentSpeed , yinput * currentSpeed);    
                                                     
    
    }                                              
}
