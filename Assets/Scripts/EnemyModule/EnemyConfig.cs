using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Game/EnemyType", order = 0)]
public sealed class EnemyConfig : ScriptableObject
{
    public int damage = 10;
    
    public int maxHealth = 100;
    
    public float moveSpeed = 2f;
    
    public float wanderRadius = 15f;
}