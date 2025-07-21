namespace PlayerModule
{
    public sealed class PlayerModel
    {
        private int _health;
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

        public int Damage => _damage;

        public float MoveSpeed => _moveSpeed;
        public float MouseSensitivity => _mouseSensitivity;
        public float ShootDistance => _shootDistance;
        public float HitRadius => _hitRadius;
        public float ShootCheckArea => _shootCheckArea;
        public float MinVerticalAngle => _minVerticalAngle;
        public float MaxVerticalAngle => _maxVerticalAngle;

        public PlayerModel()
        {
            _health = 100;
            _damage = 25;
            _moveSpeed = 5f;
            _mouseSensitivity = 0.25f;
            _shootDistance = 100f;
            _hitRadius = 0.5f;
            _shootCheckArea = 10f;
            _minVerticalAngle = -80f;
            _maxVerticalAngle = 80f;
        }
    }
}