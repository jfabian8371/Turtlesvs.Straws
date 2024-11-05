using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;//typical stuff

    public void SetTarget(Transform _target)
    {
        target = _target;//this sets the target given
    }

    private void FixedUpdate()
    {
        if (!target)
        {
            return;//if no target rerturn
        }
        Vector2 direction = (target.position - transform.position).normalized;//current obj position and target position, normalize helps control the movement speed on this

        rb.velocity = direction * bulletSpeed;//if there is though get the velocity to shoot it at
      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);//subtract health from straw and then destroy it
    }
}
