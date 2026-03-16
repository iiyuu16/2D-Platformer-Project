using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimation : MonoBehaviour
{
    Animator anims;
    SpriteRenderer sprite;
    Rigidbody2D rb;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;

    float lastDirection = 1.0f;

    private void Start()
    {
        anims = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if (jumpAction.IsPressed() && rb.velocity.y == 0.0f)
            SetAnimationTrigger("Jumped");
        if (attackAction.IsPressed())
            SetAnimationTrigger("Attacked");
        SetAnimationBool("isRunning", moveAction.ReadValue<float>() != 0.0f);
        SetAnimationBool("isGrounded", rb.velocity.y == 0.0f);

        lastDirection = moveAction.ReadValue<float>() != 0.0f ? moveAction.ReadValue<float>() : lastDirection;
        sprite.flipX = lastDirection < 0.0f;
    }

    public void SetAnimationTrigger(String triggerName)
    {
        anims.SetTrigger(triggerName);
    }

    public void SetAnimationBool(String triggerName, bool condition)
    {
        anims.SetBool(triggerName, condition);
    }

    public void SetSpriteVisibility(bool visibility)
    {
        sprite.enabled = visibility;
    }
}
