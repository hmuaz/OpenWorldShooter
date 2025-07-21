using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig")]
public sealed class PlayerConfig : ScriptableObject
{
    public int maxHealth = 100;
    public int damage = 25;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 0.25f;
    public float shootDistance = 100f;
    public float hitRadius = 0.5f;
    public float shootCheckArea = 10f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;
}