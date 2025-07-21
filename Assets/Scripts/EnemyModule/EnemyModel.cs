namespace EnemyModule
{
    public sealed class EnemyModel
    {
        private readonly EnemyConfig _config;
        private int _health;
        private int _damage;
        private float _moveSpeed;
        private float _wanderRadius;
        private float _fireCooldown;
        private float _shootRange;
        private float _shootArea;
        private float _changeTargetDistance;
        private bool _isDead;

        public EnemyConfig Config => _config;
        public int Health
        {
            get => _health;
            set => _health = value;
        }
        public int Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public float WanderRadius => _wanderRadius;
        public float FireCooldown => _fireCooldown;
        public float ShootRange => _shootRange;
        public float ShootArea => _shootArea;
        public float ChangeTargetDistance => _changeTargetDistance;
        public bool IsDead
        {
            get => _isDead;
            set => _isDead = value;
        }

        public EnemyModel(EnemyConfig config)
        {
            _config = config;
            _health = config.maxHealth;
            _damage = config.damage;
            _moveSpeed = config.moveSpeed;
            _wanderRadius = config.wanderRadius;
            _fireCooldown = 1.0f;
            _shootRange = 8f;
            _shootArea = 12f;
            _changeTargetDistance = 1.5f;
            _isDead = false;
        }
    }
}