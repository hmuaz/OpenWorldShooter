using System;
using EnemyModule;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] 
    private Transform vfxHit;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody  = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 100f;
        _rigidbody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyView>() != null)
        {
            Transform blood = Instantiate(vfxHit, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}

