using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public struct AnimationLookUpTable
{
    public int id;
    public AnimationClip animation;
    public string name;


}
public class AnimationController : MonoBehaviour
{
    public Animator animator;
    Action attack;
    public AnimationLookUpTable[] animations;
    bool attacking = false;
    string[] triggerNames = { "LookSide", "LookUp", "LookDown" };
    public float velocity = 0;
    string currentState;
    private void Start()
    {

    }

    public void MouseAngleToAnimationDir(float angle)
    {
        
        
        if (attacking == false)
        {
            if (velocity > 0.01f)
            {

                if (-220 <= angle && angle <= -120)
                {

                    PlayAnimationDirect(FindClipWithString("RunSide"));
                    currentState = triggerNames[0];
                    GetComponent<SpriteRenderer>().flipX = true;
                    //Debug.Log("Left");
                }
                else if ((-280 <= angle && angle <= -221) || (45 <= angle && angle <= 90))
                {
                    //Debug.Log("Top");
                    currentState = triggerNames[1];
                    PlayAnimationDirect(FindClipWithString("RunUp"));
                }
                else if (-45 <= angle && angle <= 44)
                {
                    PlayAnimationDirect(FindClipWithString("RunSide"));
                    currentState = triggerNames[0];
                    GetComponent<SpriteRenderer>().flipX = false;
                    //RIGHT
                }
                else if (-119 <= angle && angle <= -46)
                {
                    //Debug.Log("Bottom");
                    currentState = triggerNames[2];
                    PlayAnimationDirect(FindClipWithString("RunDown"));
                }
            }
            else
            {
                if (-220 <= angle && angle <= -120)
                {

                    PlayAnimationDirect(FindClipWithString("LookSide"));
                    currentState = triggerNames[0];
                    GetComponent<SpriteRenderer>().flipX = true;
                    //Debug.Log("Left");
                }
                else if ((-280 <= angle && angle <= -221) || (45 <= angle && angle <= 90))
                {
                    //Debug.Log("Top");
                    currentState = triggerNames[1];
                    PlayAnimationDirect(FindClipWithString("LookUp"));
                }
                else if (-45 <= angle && angle <= 44)
                {
                    PlayAnimationDirect(FindClipWithString("LookSide"));
                    currentState = triggerNames[0];
                    GetComponent<SpriteRenderer>().flipX = false;
                    //RIGHT
                }
                else if (-119 <= angle && angle <= -46)
                {
                    //Debug.Log("Bottom");
                    currentState = triggerNames[2];
                    PlayAnimationDirect(FindClipWithString("LookDown"));
                }
            }
        }

    }
    public void PlayAnimationDirect(AnimationClip animationClip)
    {
        animator.Play(animationClip.name);
    }
    //public void PlayAnimationQueued(AnimationClip animationClip)
    //{
    //    animator.PlayInFixedTime
    //}
    public AnimationClip FindClipWithString(string name)
    {
        return animations.FirstOrDefault(e => e.name == name).animation;
    }
    public void PlayAttackAnimation()
    {
        if (!attacking)
        {
            switch (currentState)
            {
                case "LookSide":
                    StartCoroutine(AttackTimer("AttackSide"));
                    break;
                case "LookUp":
                    StartCoroutine(AttackTimer("AttackUp"));
                    break;
                case "LookDown":
                    StartCoroutine(AttackTimer("AttackDown"));
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator AttackTimer(string cases)
    {
        Debug.Log($"{cases}");
        attacking = true;
        PlayAnimationDirect(FindClipWithString(cases));
        yield return new WaitForSeconds(FindClipWithString(cases).length);
        attacking = false;
    }


}
