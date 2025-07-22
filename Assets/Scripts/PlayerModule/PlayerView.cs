using UnityEngine;

namespace PlayerModule
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _shootEffect;
        [SerializeField] 
        private ParticleSystem _hitEffect;

        public void PlayShootEffect()
        {
            if (_shootEffect != null)
            {
                _shootEffect.Play();
            }
        }

        public void PlayHitEffect()
        {
            if (_hitEffect != null)
            {
                _hitEffect.Play();
            }
        }
        
        public void Move(Vector2 input, float moveSpeed)
        {
            Vector3 move = transform.right * input.x + transform.forward * input.y;
            transform.position += move.normalized * moveSpeed * Time.deltaTime;
        }

        public void Look(Vector2 lookInput, ref float xRotation, float mouseSensitivity, float minVertical, float maxVertical, Transform cameraPivot)
        {
            float mouseX = lookInput.x * mouseSensitivity;
            float mouseY = lookInput.y * mouseSensitivity;

            transform.Rotate(0f, mouseX, 0f);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minVertical, maxVertical);

            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}