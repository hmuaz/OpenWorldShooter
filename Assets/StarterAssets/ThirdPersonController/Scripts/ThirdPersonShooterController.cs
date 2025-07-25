using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _aimVirtualCamera;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (_input.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
        }
    }
}
