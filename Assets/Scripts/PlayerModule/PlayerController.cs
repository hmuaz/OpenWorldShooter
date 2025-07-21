using System.Collections.Generic;
using UnityEngine;
using Zenject;
using PlayerModule;
using EnemyModule;
using OpenWorldGame.Input;

namespace PlayerModule
{
    [RequireComponent(typeof(PlayerView))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Inject] 
        private EnemySpatialGrid _enemyGrid;
        
        [Inject]
        private InputController _inputController;

        private PlayerModel _model;
        
        private PlayerView _view;

        [SerializeField] 
        private Transform _cameraPivot;
        
        [SerializeField] 
        private Camera _playerCamera;

        private float _xRotation;
        
        private readonly List<EnemyController> _nearbyEnemies = new();
        
        

        private void Awake()
        {
            _view = GetComponent<PlayerView>();
            _model = new PlayerModel();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleMouseLook(_inputController.LookInput);
            HandleMovement(_inputController.MoveInput);

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void HandleMouseLook(Vector2 lookInput)
        {
            float mouseX = lookInput.x * _model.MouseSensitivity;
            float mouseY = lookInput.y * _model.MouseSensitivity;

            transform.Rotate(0f, mouseX, 0f);

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, _model.MinVerticalAngle, _model.MaxVerticalAngle);

            _cameraPivot.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private void HandleMovement(Vector2 moveInput)
        {
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            transform.position += move.normalized * _model.MoveSpeed * Time.deltaTime;
        }

        private void Shoot()
        {
            _nearbyEnemies.Clear();
            _enemyGrid.GetEnemiesInArea(_playerCamera.transform.position, _model.ShootCheckArea, _nearbyEnemies);

            Vector3 origin = _playerCamera.transform.position;
            Vector3 direction = _playerCamera.transform.forward;

            EnemyController closestEnemy = null;
            float closestDistance = _model.ShootDistance + 1f; 

            for (int index = 0; index < _nearbyEnemies.Count; index++)
            {
                EnemyController enemy = _nearbyEnemies[index];

                Vector3 toEnemy = enemy.Position - origin;
                float projection = Vector3.Dot(toEnemy, direction.normalized);

                if (projection < 0f || projection > _model.ShootDistance)
                {
                    continue;
                }

                Vector3 closestPoint = origin + direction.normalized * projection;
                float distanceToLine = Vector3.Distance(enemy.Position, closestPoint);

                if (distanceToLine <= _model.HitRadius && projection < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = projection;
                }
            }

            Vector3 endPoint = origin + direction * _model.ShootDistance;
            Debug.DrawLine(origin, endPoint, Color.red, 0.5f);

            if (closestEnemy != null)
            {
                closestEnemy.OnHit(_model.Damage);
                //_view.PlayShootEffect();
            }
            else
            {
                Debug.Log("Hiçbir şey vurulmadı.");
            }
        }
    }
}
