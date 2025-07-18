namespace EnemyModule
{
    public sealed class EnemyModel
    {
        private readonly EnemyType _type;
        private int _health;
        private int _damage;
        private float _moveSpeed;
        private float _wanderRadius;
        private float _fireCooldown;
        private float _shootRange;
        private float _shootArea;
        private float _changeTargetDistance;
        private bool _isDead;

        public EnemyType Type => _type;
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

        public EnemyModel(EnemyType type)
        {
            _type = type;
            _health = type.MaxHealth;
            _damage = type.Damage;
            _moveSpeed = type.MoveSpeed;
            _wanderRadius = type.WanderRadius;
            _fireCooldown = 1.0f;
            _shootRange = 8f;
            _shootArea = 12f;
            _changeTargetDistance = 1.5f;
            _isDead = false;
        }
    }
}