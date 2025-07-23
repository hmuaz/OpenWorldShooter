namespace PlayerModule
{
    public sealed class PlayerModel
    {
        private int _health;
        private int _maxHealth;
        private int _damage;
        
        private float _moveSpeed;
        private float _mouseSensitivity;
        private float _shootDistance;
        private float _hitRadius;
        private float _shootCheckArea;
        private float _minVerticalAngle;
        private float _maxVerticalAngle;

        public int Health
        {
            get => _health;
            set => _health = value;
        }
        public int MaxHealth => _maxHealth;
        public int Damage => _damage;

        public float MoveSpeed => _moveSpeed;
        public float MouseSensitivity => _mouseSensitivity;
        public float ShootDistance => _shootDistance;
        public float HitRadius => _hitRadius;
        public float ShootCheckArea => _shootCheckArea;
        public float MinVerticalAngle => _minVerticalAngle;
        public float MaxVerticalAngle => _maxVerticalAngle;

        public PlayerModel(
            int maxHealth,
            int damage,
            float moveSpeed,
            float mouseSensitivity,
            float shootDistance,
            float hitRadius,
            float shootCheckArea,
            float minVerticalAngle,
            float maxVerticalAngle)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
            _damage = damage;
            _moveSpeed = moveSpeed;
            _mouseSensitivity = mouseSensitivity;
            _shootDistance = shootDistance;
            _hitRadius = hitRadius;
            _shootCheckArea = shootCheckArea;
            _minVerticalAngle = minVerticalAngle;
            _maxVerticalAngle = maxVerticalAngle;
        }
    }
}