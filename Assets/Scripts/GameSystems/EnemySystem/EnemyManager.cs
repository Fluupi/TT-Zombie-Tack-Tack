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

    [Header("Orange Specifics")]
    [SerializeField] private Color orangeColor;
    [SerializeField] private CountDown orangeCountDown;
    [SerializeField] private float minOrangeMoveDuration;
    [SerializeField] private float maxOrangeMoveDuration;
    [SerializeField] private float minOrangeRotationSpeed;
    [SerializeField] private float maxOrangeRotationSpeed;

    [Header("Red Specifics")]
    [SerializeField] private float minRedMoveDuration;
    [SerializeField] private float maxRedMoveDuration;
    [SerializeField] private float minRedRotationSpeed;
    [SerializeField] private float maxRedRotationSpeed;

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
        
        enemies[0].gameObject.SetActive(true);
        enemies[0].Spawn(Random.Range(minOrangeMoveDuration, maxOrangeMoveDuration),
                         Random.Range(minOrangeRotationSpeed, maxOrangeRotationSpeed));
    }

    public void SpawnReds()
    {
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
}
