using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Game/EnemyType", order = 0)]
public sealed class EnemyType : ScriptableObject
{
    [SerializeField] 
    private int _damage = 10;
    
    [SerializeField] 
    private int _maxHealth = 100;
    
    [SerializeField] 
    private float _moveSpeed = 2f;
    
    [SerializeField] 
    private float _wanderRadius = 15f;

    public int Damage => _damage;
    public int MaxHealth => _maxHealth;
    
    public float MoveSpeed => _moveSpeed;
    public float WanderRadius => _wanderRadius;
}