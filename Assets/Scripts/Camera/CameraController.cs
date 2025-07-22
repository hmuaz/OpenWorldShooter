using UnityEngine;

namespace OpenWorldGame.Camera
{
    public class CameraController
    {
        private float _xRotation;

        public void HandleLook(
            Vector2 lookInput,
            Transform targetTransform,
            Transform cameraPivot,
            float mouseSensitivity,
            float minVerticalAngle,
            float maxVerticalAngle
        )
        {
            float mouseX = lookInput.x * mouseSensitivity;
            float mouseY = lookInput.y * mouseSensitivity;

            targetTransform.Rotate(0f, mouseX, 0f);

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, minVerticalAngle, maxVerticalAngle);

            cameraPivot.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }
}
