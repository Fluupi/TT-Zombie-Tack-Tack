using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Prefab needs")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParentTransform;

    [Header("Default setting")]
    [SerializeField] private Transform heroPosition;

    [Header("Spawns")]
    [SerializeField] private Transform orangeSpawn;
    [SerializeField] private Transform[] redSpawns;
    [Space]
    [SerializeField] private Color orangeColor;
    private float minOrangeMoveDuration;
    private float maxOrangeMoveDuration;
    private float minOrangeRotationSpeed;
    private float maxOrangeRotationSpeed;

    private float minRedMoveDuration;
    private float maxRedMoveDuration;
    private float minRedRotationSpeed;
    private float maxRedRotationSpeed;

    [Space]
    [SerializeField] private CountDown orangeCountDown;
    [SerializeField] private CountDown redCountDown;

    private List<Enemy> enemies;

    private void Start()
    {
        GameManager gm = GameManager.Instance;
        ;
        enemies = new List<Enemy>
        {
            Instantiate(enemyPrefab, enemyParentTransform).GetComponent<Enemy>()
        };

        enemies[0].SetUp(orangeSpawn, heroPosition, orangeColor);
        enemies[0].name = "Orange Enemy";
        enemies[0].OnExplosion.AddListener(orangeCountDown.Launch);
        enemies[0].OnExplosion.AddListener(gm.OnDamageTaken.Invoke);

        foreach (Transform spawn in redSpawns)
        {
            int enemyIndex = enemies.Count;
            enemies.Add(Instantiate(enemyPrefab, enemyParentTransform).GetComponent<Enemy>());
            Enemy e = enemies[enemyIndex];
            e.transform.position = redSpawns[enemyIndex-1].position;
            e.SetUp(redSpawns[enemyIndex-1], heroPosition, Color.red);
            e.name = $"Red Enemy {enemyIndex}";
            e.gameObject.SetActive(false);
            e.OnExplosion.AddListener(gm.OnDamageTaken.Invoke);
        }
    }

    public void SpawnOrange()
    {
        if(!GameManager.Instance.Running)
            return;

        Debug.Log("Spawn orange");

        enemies[0].gameObject.SetActive(true);
        enemies[0].Spawn(Random.Range(minOrangeMoveDuration, maxOrangeMoveDuration),
                         Random.Range(minOrangeRotationSpeed, maxOrangeRotationSpeed));
    }

    public void SpawnReds()
    {
        Debug.Log("Spawn reds");
        for (int i = 1; i < enemies.Count; i++)
        {
            enemies[i].gameObject.SetActive(true);
            enemies[i].Spawn(Random.Range(minRedMoveDuration, maxRedMoveDuration),
                Random.Range(minRedRotationSpeed, maxRedRotationSpeed));
        }
    }
    
    public void KillAllEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void DataUpdate(GameMode gm)
    {
        minOrangeMoveDuration = gm.orangeMinEnemyMoveDuration;
        maxOrangeMoveDuration = gm.orangeMaxEnemyMoveDuration;
        minRedMoveDuration = gm.redMinEnemyMoveDuration;
        maxRedMoveDuration = gm.redMaxEnemyMoveDuration;
    }
}
