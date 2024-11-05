using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;//have it public so others can use


    public Transform startPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 200;//give 200 'gold' to start
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;//when this is called, a straw is destroyed, it gives the player more gold
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        {
            //buy tower
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough gold to buy");
            return false;//if there isn;t enough to buy the tower return false
        }
    }

}
