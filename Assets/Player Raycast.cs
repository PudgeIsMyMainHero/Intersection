using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    Movement movement;
    [SerializeField]private LayerMask hitLayer;

    private void Awake()
    {
        playerInput = new();
    }
    private PlayerInput playerInput;
    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Use.performed += OnUse;
        playerInput.Player.Deuse.performed += OnDeuse;
    }
    private void OnDisable()
    {
        playerInput.Player.Use.performed -= OnUse;
        playerInput.Player.Deuse.performed -= OnDeuse;
        playerInput.Player.Disable();
    }
    public void OnDeuse(InputAction.CallbackContext context)
        {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, hitLayer))
        {
            if (hit.transform.TryGetComponent<Movement>(out movement))
            {
                movement.MoveBack();
            }
        }
    }
    public void OnUse(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, hitLayer))
        {
            if (hit.transform.TryGetComponent<Movement>(out movement))
            {
                movement.Move();
            }
        }
    }
    
}
