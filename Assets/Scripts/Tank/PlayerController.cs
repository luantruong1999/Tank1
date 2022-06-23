using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput;
    private bool _fireInput;
    public Vector2 MoveInput => _moveInput;
    public bool FireInput => _fireInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x == 0 || context.ReadValue<Vector2>().y == 0)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _fireInput = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            _fireInput = false;
        }
    }
}
