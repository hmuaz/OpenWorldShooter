using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _aimVirtualCamera;
    
    [SerializeField]
    private LayerMask _aimColliderMask;
    
    [SerializeField] 
    private Transform _debugTransform;

    private StarterAssetsInputs _input;

    private ThirdPersonController _thirdPersonController;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        
        
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderMask))
        {
            _debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        
        
        
        if (_input.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.RotateOnMove = false;
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.RotateOnMove = true;
        }
    }
    
}
