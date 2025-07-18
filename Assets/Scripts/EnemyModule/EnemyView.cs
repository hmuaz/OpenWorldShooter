using UnityEngine;

namespace EnemyModule
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] 
        private Color _hitColor = Color.red;
        
        [SerializeField] 
        private float _hitEffectDuration = 0.15f;
        
        [SerializeField] 
        private ParticleSystem _deathParticle;

        [SerializeField]
        private Renderer _renderer;
        
        private Color _originalColor;
        
        private float _effectTimer = 0f;
        
        private bool _isHitEffectActive = false;

        private void Awake()
        {
            _renderer = transform.GetChild(0).GetComponent<Renderer>();
            if (_renderer != null)
            {
                _originalColor = _renderer.material.color;
            }
        }

        private void Update()
        {
            if (_isHitEffectActive)
            {
                _effectTimer -= Time.deltaTime;
                if (_effectTimer <= 0f)
                {
                    ResetColor();
                }
            }
        }

        public void PlayHitEffect()
        {
            if (_renderer == null)
            {
                return;
            }
            _renderer.material.color = _hitColor;
            _effectTimer = _hitEffectDuration;
            _isHitEffectActive = true;
        }

        public void PlayDeathEffect()
        {
            if (_deathParticle != null)
            {
                _deathParticle.transform.SetParent(null); 
                _deathParticle.Play();
                Destroy(_deathParticle.gameObject, 2f); 
            }
        }

        private void ResetColor()
        {
            if (_renderer == null) { return; }
            _renderer.material.color = _originalColor;
            _isHitEffectActive = false;
        }
    }
}