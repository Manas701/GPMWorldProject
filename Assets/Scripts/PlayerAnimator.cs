using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.current.onPlayerMove += playMoveAnimation;
        GameManager.current.onPlayerStopMove += playIdleAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMoveAnimation()
    {
        if (isMoving == false)
        {
            isMoving = true;
            anim.SetTrigger("triggerMove");
            anim.SetBool("isMoving", isMoving);
        }
    }

    public void playIdleAnimation()
    {
        if (isMoving == true)
        {
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
        }
    }
}
