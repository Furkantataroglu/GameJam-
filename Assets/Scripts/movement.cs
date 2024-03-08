using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float swimSpeed = 5f; // Speed of the player while swimming
    public float acceleration = 2f; // Acceleration rate
    public float deceleration = 5f; // Deceleration rate
    private float currentSpeed;

    private Rigidbody2D b;

    private void Start()
    {
        b = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Input handling for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movementInput = new Vector2(horizontalInput, verticalInput).normalized;

        // Calculate current velocity direction
        Vector2 velocityDirection = b.velocity.normalized;

        // Calculate desired velocity direction
        Vector2 desiredVelocityDirection = movementInput.normalized;
 Debug.Log("velocity "+ b.velocity );
        // Apply acceleration
        if (movementInput.magnitude > 0)
        {
            //Debug.Log(movementInput.magnitude );
            
            b.velocity += desiredVelocityDirection * swimSpeed * acceleration * Time.fixedDeltaTime;
        }
        else
        {
             //Debug.Log(movementInput.magnitude);
            // Decelerate smoothly
            b.velocity -= b.velocity * deceleration * Time.fixedDeltaTime;
        }

        // Clamp speed to the maximum swim speed
        b.velocity = Vector2.ClampMagnitude(b.velocity, swimSpeed);
    }
}