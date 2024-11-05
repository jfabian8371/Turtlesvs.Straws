using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator animate;//this is for the shop to go in and out

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        animate.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    public void SetSelected()
    {

    }//used inside unity
}
