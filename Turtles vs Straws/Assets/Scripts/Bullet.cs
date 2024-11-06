using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip shootingSound;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private const float maxDistance = 220f;
    private Vector2 startPosition;
    private Transform target;//typical stuff

    public void SetTarget(Transform _target)
    {
        target = _target;//this sets the target given
        startPosition = target.position;//get what the position of bullet is to begin
    }

    private void FixedUpdate()
    {
        if (!target)
        {
            return;//if no target rerturn
        }
        Vector2 direction = (target.position - transform.position).normalized;//current obj position and target position, normalize helps control the movement speed on this

        rb.velocity = direction * bulletSpeed;//if there is though get the velocity to shoot it at
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);
        if (distanceTraveled > maxDistance)
        {
            Destroy(gameObject);//delete bullet if it goes too far
        }
        AudioSource.PlayClipAtPoint(shootingSound, transform.position, 0.2f);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);//subtract health from straw and then destroy it
    }
}
