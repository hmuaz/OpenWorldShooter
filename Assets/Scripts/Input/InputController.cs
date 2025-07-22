using UnityEngine;
using UnityEngine.InputSystem;

namespace OpenWorldGame.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputController : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        
        private bool _shootTriggered;
        private bool _isShootingHeld;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;

        public bool ShootTriggered => _shootTriggered;
        public bool IsShootingHeld => _isShootingHeld;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.Enable();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;
            _inputActions.Player.Look.performed += OnLook;
            _inputActions.Player.Look.canceled += OnLook;
            _inputActions.Player.Shoot.performed += OnShootStarted;
            _inputActions.Player.Shoot.canceled += OnShootCanceled;
        }

        private void OnDisable()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMove;
            _inputActions.Player.Look.performed -= OnLook;
            _inputActions.Player.Look.canceled -= OnLook;
            _inputActions.Player.Shoot.performed -= OnShootStarted;
            _inputActions.Player.Shoot.canceled -= OnShootCanceled;

            _inputActions.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        private void OnShootStarted(InputAction.CallbackContext context)
        {
            _shootTriggered = true;
            _isShootingHeld = true;
        }

        private void OnShootCanceled(InputAction.CallbackContext context)
        {
            _isShootingHeld = false;
        }

        private void LateUpdate()
        {
            _shootTriggered = false;
        }
    }
}
