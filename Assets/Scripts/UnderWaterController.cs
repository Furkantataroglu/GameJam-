using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header ("Swimming")]
    [SerializeField] private float speed  = 0f; //walking speed

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

    // Calculate the target speed based on input
    float targetSpeed = Mathf.Sqrt(xinput * xinput + yinput * yinput) * speed;

    // Apply acceleration or deceleration
    currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, (currentSpeed < targetSpeed) ? accelerationLerp : decelerationLerp);

    // Clamp the current speed to the maximum speed
    currentSpeed = Mathf.Clamp(currentSpeed, 0, speed);

    // Update the velocity
    rb.velocity = new Vector2(xinput * currentSpeed, yinput * currentSpeed);
}

   private void RotatePlayer()
{
    var xinput = Input.GetAxisRaw("Horizontal");
    var yinput = Input.GetAxisRaw("Vertical");

    if (xinput > 0)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); //Turn right
    }
    else if (xinput < 0)
    {
        transform.rotation = Quaternion.Euler(0, 180, 0); // turn left
    }
}

}
