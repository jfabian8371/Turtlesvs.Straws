using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turtle : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;//fields for the specifics of my turtle and the gun with each turtle

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f;//bullet per second = bps

    private Transform target;
    private float timeUntilFire;//

    private void Update()
    {
        if (target == null)//that means we need to go find a new one
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();//if there is a target it rotates towards it

        if (!CheckTargetIsInRange())
        {
            target = null;//just because we found a target doesn't mean its in range so also check that
        }
        else//if in target add to timeuntilfire, check if it is ready for another shot
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }//create the bullet, run the script, then set the target 

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);//searches the circle to detect an enemy
        if (hits.Length > 0)
        { //means it hit something
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()//this is getting the true or false if it is in the range of the turtle
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;//this gets the angle that needs to be rotated
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));//creates the rotation from the angle given
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);//smoothly rotate to that target
    }


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }//visual target area

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
