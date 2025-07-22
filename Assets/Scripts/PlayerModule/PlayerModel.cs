namespace PlayerModule
{
    public sealed class PlayerModel
    {
        private int _health;
        
        private readonly PlayerConfig _config;

        public int Health
        {
            get => _health;
            set => _health = value;
        }
        public int MaxHealth => _config.maxHealth;
        public int Damage => _config.damage;
        
        public float MoveSpeed => _config.moveSpeed;
        public float MouseSensitivity => _config.mouseSensitivity;
        public float ShootDistance => _config.shootDistance;
        public float HitRadius => _config.hitRadius;
        public float ShootCheckArea => _config.shootCheckArea;
        public float MinVerticalAngle => _config.minVerticalAngle;
        public float MaxVerticalAngle => _config.maxVerticalAngle;

        public PlayerModel(PlayerConfig config)
        {
            _config = config;
            _health = config.maxHealth;
        }
    }
}