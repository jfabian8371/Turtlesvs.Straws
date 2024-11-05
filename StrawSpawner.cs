using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class StrawSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] strawPrefabs;
    [SerializeField] private AudioClip waveStartSound;
    [SerializeField] private AudioClip waveEndSound;
    [SerializeField] private Text roundCounterText;

    [Header("Attributes")]
    [SerializeField] private int baseStraws = 8;
    [SerializeField] private float strawsPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float strawsPerSecondCap = 15f;//values to use later when setting number of straws in a wave, persecond, between waves, difficulty factor

    [Header("Events")]
    public static UnityEvent onStrawDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int strawsAlive;
    private int strawsLeftToSpawn;
    private float eps;
    private bool isSpawning = false;//some more values to use later on 

    private void Awake()
    {
        onStrawDestroy.AddListener(StrawDestroyed);//call strawdestroyed everytime a straw is destroyed
    }

    private void Start()
    {
        StartCoroutine(StartWave());//start first wave
    }
    private void Update()
    {
        if (!isSpawning)
        {
            return;//checks to see if we are spawing straws still
        }

        timeSinceLastSpawn += Time.deltaTime;//add to total time

        if (timeSinceLastSpawn >= (1f / eps) && strawsLeftToSpawn > 0)//check if time since last spawn is ready to go again and if there are straws left
        {
            SpawnEnemy();
            strawsLeftToSpawn--;//call spawn, descrease left to spawn, increase alive, and then set the time to 0
            strawsAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (strawsAlive == 0 && strawsLeftToSpawn == 0)
        {
            AudioSource.PlayClipAtPoint(waveEndSound, transform.position, 3f);
            EndWave();//end wave if both are 0, means all destroyed and none left to spawn
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        AudioSource.PlayClipAtPoint(waveStartSound, transform.position, 1f);

        isSpawning = true;
        strawsLeftToSpawn = StrawsPerWave();
        eps = strawsPerSecond;//wait for specific time between waves, start spawning

        if (roundCounterText != null)
        {
            roundCounterText.text = "Round: " + currentWave;
        }
    }

    private void StrawDestroyed()
    {
        strawsAlive--;//decrease amount of straws alive cuz one got destroyed
    }

    private void EndWave()
    {
     
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;//set values to what they should be when the wave is done and then start a new wave
        StartCoroutine(StartWave());
    }
    private void SpawnEnemy()
    {
        int index = Random.Range(0, strawPrefabs.Length);//select random number to pick on what the enemies are
        GameObject prefabToSpawn = strawPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);//take that random straw and spawn it in
    }

    private int StrawsPerWave()
    {
        return Mathf.RoundToInt(baseStraws * Mathf.Pow(currentWave, difficultyScalingFactor));//return what the calculation for each wave is when called
    }//scales up every wave

    private float StrawsPerSecond()
    {
        return Mathf.Clamp(strawsPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, strawsPerSecondCap);//gets spawn rate of straws for current wave
    }

}
