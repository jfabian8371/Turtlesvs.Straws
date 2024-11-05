using System;
using UnityEngine;


[System.Serializable]
public class Tower//this is a file to essentially used in the build process of whichever tower(not named turtle because I already made a file named that and didn't want to change it haha)
{//going to set the fields and stuff nothing crazy
    public string name;
    public int cost;
    public GameObject towerPrefab;

    public Tower (string _name, int _cost, GameObject _towerPrefab)
    {
        name = _name;
        cost = _cost;
        towerPrefab = _towerPrefab;
    }
}
