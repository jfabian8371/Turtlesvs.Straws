using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    public string LevelName;//scene for game
    

    public void LoadLevel()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, 1f);
        SceneManager.LoadScene(LevelName);
    }
}
