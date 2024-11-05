using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float movementSpeed = 2.0f;

    private Transform target;
    private int pathPosition = 0;
    private void Start()
    {
        target = LevelManager.main.path[pathPosition];//get the target
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)//check distance
        {
            pathPosition++;

            if (pathPosition == LevelManager.main.path.Length)
            {
                LevelManager.main.RegisterStrawAtEnd();

                StrawSpawner.onStrawDestroy.Invoke();//call to destroy enemies
                Destroy(gameObject);
                return;//means it hit the end
            } else{
                target = LevelManager.main.path[pathPosition];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;//make it between 0 and 1
        rb.velocity = direction * movementSpeed;


    }

}
