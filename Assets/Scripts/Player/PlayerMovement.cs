using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D coll;

    InputAction moveAction;
    InputAction jumpAction;

    public LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpHeight = 1.0f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        rb.AddForce(new Vector2(moveAction.ReadValue<float>() * speed * Time.deltaTime, 0), ForceMode2D.Impulse);

        if (jumpAction.IsPressed() && IsGrounded())
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckDistance, groundLayer);

        if (hit.collider != null)
            return true;

        return false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
    }
}
