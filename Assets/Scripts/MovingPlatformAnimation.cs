using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAnimation : MonoBehaviour
{
    public bool Gray;

    private Animator animator;
    private AnimationState state;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (Gray)
            state = AnimationState.Platform_Moving_Gray;
        else
            state = AnimationState.Platform_Moving;

        if(Gray)
            animator.SetTrigger("Gray");
    }
    private void FixedUpdate()
    {
        ChangeAnimationState(Gray ? "Platform_Moving_Gray": "Platform_Moving");
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
public enum AnimationState { Platform_Moving, Platform_Moving_Gray}