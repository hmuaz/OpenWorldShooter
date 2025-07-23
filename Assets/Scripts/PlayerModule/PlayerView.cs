using UnityEngine;

namespace PlayerModule
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _shootEffect;
        [SerializeField] 
        private ParticleSystem _hitEffect;
        
        [SerializeField]
        private Transform _cameraPivot;
        
        [SerializeField]
        private Transform _playerCamera;
        
        private PlayerModel _playerModel;
        
        public Transform PlayerCamera => _playerCamera;
        public Transform CameraPivot => _cameraPivot;
        public PlayerModel Model => _playerModel;
        
        

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

        public void SetModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
    }
}