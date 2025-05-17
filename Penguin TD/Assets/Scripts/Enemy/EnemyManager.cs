using UnityEngine;
using static UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{

    public static EnemyManager main;

    [Header("Route")]
    public Transform spawnpoint;
    public Transform[] checkpoints;
    [Header("Spawner Settings")]
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private float fishRate = 0.4f;
    [SerializeField] private float fastFishRate = 0.4f;
    [SerializeField] private float tankFishRate = 0.2f;
    [SerializeField] private int wave = 1;
    [SerializeField] private float enemyIncreaseRate = 0.2f;
    [SerializeField] private float maxSpawnDelay = 1.0f;
    [SerializeField] private float minSpawnDelay = 0.75f;
    [SerializeField] private float waveDelayTime = 3.0f;
    [Header("Enemy List")]
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject fastFish;
    [SerializeField] private GameObject tankFish;
    [SerializeField] private GameObject superSpeedFish;

    [NonSerialized] public bool gameStart = false;
    [NonSerialized] public bool wavedone = false;

    private List<GameObject> waveset = new List<GameObject>();
    private int enemiesLeft;
    private int fishCount;
    private int fastFishCount;
    private int tankFishCount;
    private int superSpeedFishCount;
    private float waveTimer = 0;

    void Awake() {
        main = this; 
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (wavedone && enemies.Length == 0)
        {
            if (waveTimer > waveDelayTime)
            {
                ++wave;
                wavedone = false;
                enemyCount += Mathf.RoundToInt(enemyCount * enemyIncreaseRate);
                StartWave();
                waveTimer = 0;
            }
            else
            {
                waveTimer += Time.deltaTime;
            }
        }
    }

    public void StartWave() {
        fishCount = Mathf.RoundToInt(enemyCount * fishRate);
        fastFishCount = Mathf.RoundToInt(enemyCount * fastFishRate);
        tankFishCount = Mathf.RoundToInt(enemyCount * tankFishRate);
        superSpeedFishCount = 0;

        if(wave % 2 == 1) {
            superSpeedFishCount = wave / 2;
        }

        enemiesLeft = fishCount + fastFishCount + tankFishCount + superSpeedFishCount;
        enemyCount = enemiesLeft;

        waveset = new List<GameObject>();
        for(int i = 0; i < fishCount; ++i) {
            waveset.Add(fish);
        }
        for(int i = 0; i < fastFishCount; ++i) {
            waveset.Add(fastFish);
        }
        for(int i = 0; i < tankFishCount; ++i) {
            waveset.Add(tankFish);
        }
        for(int i = 0; i < superSpeedFishCount; ++i) {
            waveset.Add(superSpeedFish);
        }

        waveset = Shuffle(waveset);

        StartCoroutine(spawn());
    }

    public List<GameObject> Shuffle(List<GameObject> waveSet) {
        List<GameObject> temp = new List<GameObject>();
        List<GameObject> result = new List<GameObject>();

        temp.AddRange(waveSet);

        for(int i = 0; i < waveSet.Count; ++i) {
            int index = Random.Range(0, temp.Count - 1);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        
        return result;
    }

    IEnumerator spawn() {
        for(int i = 0; i < waveset.Count; ++i) {
            Instantiate(waveset[i], spawnpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
        wavedone = true;
    }
}
