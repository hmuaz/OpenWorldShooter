using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class PlayerController : MonoBehaviour
{
    private const float _distanceEpsilon = 1f;
    
    [Inject] 
    private EnemySpatialGrid _enemyGrid;

    [SerializeField] 
    private int _damage = 25;
    
    [SerializeField] 
    private Transform _cameraPivot;
    
    [SerializeField] 
    private Camera _playerCamera;
    
    [SerializeField] 
    private float _shootDistance = 100f;
    [SerializeField] 
    private float _hitRadius = 0.5f;
    [SerializeField] 
    private float _shootCheckArea = 10f; 
    [SerializeField] 
    private float _moveSpeed = 5f;
    [SerializeField] 
    private float _mouseSensitivity = 2f;
    [SerializeField] 
    private float _minVerticalAngle = -80f;
    [SerializeField] 
    private float _maxVerticalAngle = 80f;


    private float _xRotation;
    
    private readonly List<Enemy> _nearbyEnemies = new();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        transform.Rotate(0f, mouseX, 0f);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minVerticalAngle, _maxVerticalAngle);

        _cameraPivot.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        transform.position += move.normalized * _moveSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _nearbyEnemies.Clear();
        _enemyGrid.GetEnemiesInArea(_playerCamera.transform.position, _shootCheckArea, _nearbyEnemies);

        Vector3 origin = _playerCamera.transform.position;
        Vector3 direction = _playerCamera.transform.forward;

        Enemy closestEnemy = null;
        float closestDistance = _shootDistance + _distanceEpsilon;

        for (int index = 0; index < _nearbyEnemies.Count; index++)
        {
            Enemy enemy = _nearbyEnemies[index];
            Vector3 toEnemy = enemy.transform.position - origin;
            float projection = Vector3.Dot(toEnemy, direction.normalized);

            if (projection < 0f || projection > _shootDistance)
            {
                continue;
            }

            Vector3 closestPoint = origin + direction.normalized * projection;
            float distanceToLine = Vector3.Distance(enemy.transform.position, closestPoint);

            if (distanceToLine <= _hitRadius && projection < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = projection;
            }
        }

        Vector3 endPoint = origin + direction * _shootDistance;
        Debug.DrawLine(origin, endPoint, Color.red, 0.5f);

        if (closestEnemy != null)
        {
            closestEnemy.OnHit(_damage);
        }
        else
        {
            Debug.Log("Hiçbir şey vurulmadı.");
        }
    }
}
