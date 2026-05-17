using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerAttackHitbox attackHitbox;
    private float[] hitboxPositions = new float[2] { -0.88f, 0.88f };
    private int currentHitboxPosition = 1;
    InputAction attackAction;
    InputAction moveAction;

    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        if (moveAction.ReadValue<float>() != 0.0f)
        {
            currentHitboxPosition = moveAction.ReadValue<float>() > 0 ? 1 : 0;
            attackHitbox.ToggleSpriteFlip(moveAction.ReadValue<float>() < 0);
        }
            
        attackHitbox.transform.localPosition = new Vector3(hitboxPositions[currentHitboxPosition], 0.591f, 0.0f);

        if (attackAction.IsPressed())
            attackHitbox.AttackOnHitbox();
            
    }

    public void ShowAttackSprite()
    {
        ToggleAttackSprite(true);
    }
    public void HideAttackSprite()
    {
        ToggleAttackSprite(false);
    }
    public void ToggleAttackSprite(bool visibility)
    {
        attackHitbox.ToggleSprite(visibility);
    }
}
