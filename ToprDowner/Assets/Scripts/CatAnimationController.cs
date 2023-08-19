using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CatAnimationController : MonoBehaviour
{
    public Vector3 nextWayPoint = Vector3.zero;
    public AIPath aiPath;
    public AnimationLookUpTable[] walkAnimations;
    public AnimationLookUpTable[] idleAnimations;

    bool isIdleNow = false;

    public Animator animator;

    public void Update()
    {
        Debug.Log(Mathf.Abs(aiPath.desiredVelocity.magnitude));
        if (Mathf.Abs(aiPath.velocity.magnitude) > 0.1)
        {
            if (isIdleNow)
            {
                StartCoroutine(RestartAnim());
                isIdleNow = false;
            }
            Vector2 steeringDir = aiPath.desiredVelocity;
            steeringDir.Normalize();
            float angle = Mathf.Atan2(-steeringDir.x, steeringDir.y) * Mathf.Rad2Deg-90-180;
            SteeringAngleToAnimationDir(angle);
        }
        else
        {
            if (!isIdleNow)
            {

                PlayIdleAnimation();
            }
        }

    }

    public void PlayIdleAnimation()
    {
        isIdleNow = true;
        
        PlayAnimationDirect(FindClipWithString("IdleFront", true));
    }
    IEnumerator RestartAnim()
    {
        animator.enabled = false;
        yield return new WaitForEndOfFrame();
        isIdleNow = false;
        animator.enabled = true;
    }

    public void PlayAnimationDirect(AnimationClip animationClip)
    {
        animator.Play(animationClip.name);
    }
    public AnimationClip FindClipWithString(string name, bool isIdle)
    {
        if (isIdle)
        {
            return idleAnimations.FirstOrDefault(e => e.name == name).animation;
        }
        else
        {
            return walkAnimations.FirstOrDefault(e => e.name == name).animation;
        }
    }
    public void SteeringAngleToAnimationDir(float angle)
    {
        if (angle < -280)
        {
            angle = 90 - ((angle * -1) - 280);
        }
       
            Debug.Log(angle);
;
        if (-120 <= angle && angle <= -45)
        {

            PlayAnimationDirect(FindClipWithString("WalkFront", false));


            //Debug.Log("Front");
        }
        else if ((-170 <= angle && angle <= -121))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkFrontLeft", false));
        }
        else if ((-210 <= angle && angle <= -171))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkLeft", false));
        }
        else if ((-250 <= angle && angle <= -211))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkBackLeft", false));
        }
        else if ((-269 <= angle && angle <= -251) || (60 <= angle && angle <= 89))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkBack", false));
        }
        else if ((45 <= angle && angle <= 61))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkBackRight", false));
        }
        else if ((-21 <= angle && angle <= 46))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkRight", false));
        }

        else if ((-44 <= angle && angle <= -22))
        {
            //Debug.Log("Top");

            PlayAnimationDirect(FindClipWithString("WalkFrontRight", false));
        }



    }
}
