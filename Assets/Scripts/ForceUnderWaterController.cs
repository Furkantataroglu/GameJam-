using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUnderWaterController : MonoBehaviour
{
    [Header ("Swimming")]
    [SerializeField] private float maxAccelerationForce ; // Limit the acceleration force
    [SerializeField] private float maxDecelerationForce; // Limit the deceleration force
    [SerializeField] private float maxSpeed; // Limit the max speed
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }

    // Update is called once per frame
    void Update()
    {
        ManageWalking();
        RotatePlayer();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        var xinput = Input.GetAxisRaw("Horizontal");
        var yinput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("IsMovingForward", Mathf.Abs(xinput));
        animator.SetFloat("IsMovingUp", yinput);
    }

    private void ManageWalking()
    {
        var xinput = Input.GetAxisRaw("Horizontal");
        var yinput = Input.GetAxisRaw("Vertical");

        if (xinput == 0 && yinput == 0)
        {
            // Calculate deceleration force
            Vector2 currentVelocity = rb.velocity.normalized;
            Vector2 decelerationForce = -currentVelocity * maxDecelerationForce;

            // Limit the deceleration force
            if (decelerationForce.magnitude > maxDecelerationForce)
            {
                decelerationForce = decelerationForce.normalized * maxDecelerationForce;
            }

            rb.AddForce(decelerationForce, ForceMode2D.Force);
        }
        else
        {
            // Calculate acceleration force
            Vector2 accelerationDirection = new Vector2(xinput, yinput).normalized;
            Vector2 accelerationForce = accelerationDirection * maxAccelerationForce;

            // Limit the acceleration force
            if (accelerationForce.magnitude > maxAccelerationForce)
            {
                accelerationForce = accelerationForce.normalized * maxAccelerationForce;
            }

            rb.AddForce(accelerationForce, ForceMode2D.Force);

            // Limit the speed
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    private void RotatePlayer()
    {
        var xinput = Input.GetAxisRaw("Horizontal");

        if (xinput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Right
        }
        else if (xinput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Left
        }
    }
}
