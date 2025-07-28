using System;
using System.Collections;
using EnemyModule;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Transform headTarget;
    public Enemy enemy;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        enemy = FindAnyObjectByType<Enemy>();
        headTarget = enemy.headTarget;
    }

    private void Start()
    {
        float speed = 100f;
        _rigidbody.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Head"))
        {
            Debug.Log("head");

            if (enemy != null)
            {
                Vector3 localHitDir = enemy.transform.InverseTransformDirection(_rigidbody.linearVelocity.normalized);

                float x = Mathf.Clamp(localHitDir.x, -0.5f, 0.5f);
                float y = localHitDir.z < 0 ? 2.1f : 1.4f;
                float z = enemy.headTarget.localPosition.z;

                Vector3 hitPos = new Vector3(x, y, z);

                if (enemy.headMoveRoutine != null)
                    enemy.StopCoroutine(enemy.headMoveRoutine);

                enemy.headMoveRoutine = enemy.StartCoroutine(enemy.HeadHitReaction(hitPos, 0.08f, 0.2f));

                enemy.enemyHealth -= 50;
                
                if (enemy.enemyHealth <= 0)
                {
                    if (localHitDir.z < 0)
                        enemy.PlayDeathAnimation("HeadshotFromFrontDeath"); 
                    else
                        enemy.PlayDeathAnimation("HeadshotFromBackDeath");
                    
                        
                }
            }
        }

        Destroy(gameObject);
    }
}