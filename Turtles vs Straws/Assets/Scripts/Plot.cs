using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;//for the color of the plots
    [SerializeField] private Color hoverColor;

    private GameObject turtle;
    private Color startColor;//get a turtle

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;//at start set color, at mouse enter make it a little lighter for hover
    }

    private void OnMouseExit()
    {
        sr.color = startColor;//once off that go back to normal
    }

    private void OnMouseDown()
    {
        if (turtle != null)//if this happens means you want to place a tower, so if there already is one return
        {
            return;
        }

        Tower turtleToBuild = BuildManager.main.GetSelectedTurtle();//means there is no tower so it creates one from whichever button was picked

        if (turtleToBuild.cost > LevelManager.main.currency)
        {
            return;//if you do not have enough money return nothing
        }
        LevelManager.main.SpendCurrency(turtleToBuild.cost);

        turtle = Instantiate(turtleToBuild.towerPrefab, transform.position, Quaternion.identity);
    }

}
