using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController groundController;
    public ForceUnderWaterController underWaterController;
    public Animator animator;

    public bool isSwimming = false;
    
    // Start is called before the first frame update
    void Start()
    {
        groundController.SetAnimator(animator);
        underWaterController.SetAnimator(animator);
    }

    // Update is called once per frame
    void Update()
    {
        //temp
        if (!isSwimming)
        {
            groundController.enabled = true;
            underWaterController.enabled = false;
            animator.SetBool("IsSwimming", false);
        }
        else
        {
            animator.SetBool("IsSwimming", true);
            groundController.enabled = false;
            underWaterController.enabled = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
