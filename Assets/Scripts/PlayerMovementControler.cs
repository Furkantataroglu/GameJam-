using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header ("Swimming")]
    [SerializeField] private float speed; //walking speed
    [SerializeField] private float rotationspeed; 
    [SerializeField] private float currentSpeed; 
    [SerializeField] private float accelerationLerp; 
    [SerializeField] private float decelerationLerp; 
    
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
        RotatePlayer();
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

   private void RotatePlayer()
{
    var xinput = Input.GetAxisRaw("Horizontal");
    var yinput = Input.GetAxisRaw("Vertical");

    if (xinput > 0)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); // Sağa dön
    }
    else if (xinput < 0)
    {
        transform.rotation = Quaternion.Euler(0, 180, 0); // Sola dön
    }
}

}
