using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;//this is where the building actual happens

    private int selectedTurtle = 0;

    private void Awake()
    {
        main = this;
        Debug.Log("BuildManager main initialized.");//was having issues with this so added to debug
    }

    public Tower GetSelectedTurtle()
    {
        return towers[selectedTurtle];//give prefab to spawn when picked
    }

    public void SetSelectedTower(int _selectedTurtle)
    {
        selectedTurtle = _selectedTurtle;//set the turtle tower to what was clicked
    }
}
