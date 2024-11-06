using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private AudioClip death_sound;

    private bool isDestroyed = false;//want false first for the if loop


    public void TakeDamage(int damage)
    {
        hitPoints -= damage;//subtract damage

        if (hitPoints <= 0 && !isDestroyed)//check if hitpoints is 0 or less, its dead, and if it is destroyed is false
        {
            StrawSpawner.onStrawDestroy.Invoke();//tell spawner it is destoryed
            LevelManager.main.IncreaseCurrency(50);//add 50 gold everytime an enemy is destroy
            isDestroyed = true;
            AudioSource.PlayClipAtPoint(death_sound, transform.position, 1f);
            Destroy(gameObject);
           
        }
    }
}
