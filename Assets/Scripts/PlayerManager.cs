using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player groundController;
    public ForceUnderWaterController underWaterController;
    public Animator animator;

    public bool isSwimming = false;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _cachedGravity = rb.gravityScale;
        groundController.SetAnimator(animator);
        underWaterController.SetAnimator(animator);
    }

    private float _cachedGravity;

    // Update is called once per frame
    void Update()
    {
        //temp
        if (!isSwimming)
        {
            groundController.enabled = true;
            underWaterController.enabled = false;
            animator.SetBool("IsSwimming", false);
            rb.gravityScale = _cachedGravity;
        }
        else
        {
            animator.SetBool("IsSwimming", true);
            groundController.enabled = false;
            underWaterController.enabled = true;
            rb.gravityScale = 0f;
        }
    }
}
