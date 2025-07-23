using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EnemyModule
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] 
        private Color _hitColor = Color.red;
        
        [SerializeField] 
        private float _hitEffectDuration = 0.15f;
        
        [SerializeField] 
        private ParticleSystem _deathParticle;

        [SerializeField]
        private Renderer _renderer;
        
        [Inject]
        private SignalCenter _signalCenter;
        
        private Color _originalColor;
        
        private float _effectTimer = 0f;
        private float _fireTimer;
        
        private bool _isHitEffectActive = false;
        
        private Vector3 _targetPosition;
        
        private EnemyModel _model;
        
        public float FireTimer
        {
            get => _fireTimer;
            set => _fireTimer = value;
        }
        
        public EnemyModel  Model => _model;
        
        public Vector3 TargetPosition
        {
            get => _targetPosition;
            set => _targetPosition = value;
        }

        public Vector2Int CurrentGridCell
        {
            get; private set;
        }
        public Vector3 Position => transform.position;
        public bool IsDead => Model.IsDead;

        public void SetGridCell(Vector2Int cell) => CurrentGridCell = cell;

        private void Awake()
        {
            _renderer = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
            if (_renderer != null)
            {
                _originalColor = _renderer.material.color;
            }
        }

        private void Update()
        {
            if (IsDead)
            {
                return;
            }
            
            if (_isHitEffectActive)
            {
                _effectTimer -= Time.deltaTime;
                if (_effectTimer <= 0f)
                {
                    ResetColor();
                }
            }
        }
        
        public void OnHit(int damage)
        {
            if (IsDead)
            {
                return;
            }
            
            PlayHitEffect();
            
            Model.Health -= damage;
            
            if (Model.Health <= 0)
            {
                Die();
            }
        }
        
        public void SetModel(EnemyModel model)
        {
            _model =  model;
        }
        
        private void Die()
        {
            if (IsDead)
            {
                return;
            }
            Model.IsDead = true;
            
            _signalCenter.Fire(new EnemyDiedSignal(this));
            
            Destroy(gameObject);
        }
        
        //Visual Effects
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

        private void ResetColor()
        {
            if (_renderer == null)
            {
                return;
            }
            _renderer.material.color = _originalColor;
            _isHitEffectActive = false;
        }
        
        
    }
}