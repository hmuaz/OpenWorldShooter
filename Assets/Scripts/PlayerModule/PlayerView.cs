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
    }
}