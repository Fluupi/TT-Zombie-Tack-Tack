using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private SpriteRenderer[] graph;

    [Header("Life Data")]
    [SerializeField] private Transform spawn;
    [SerializeField] private bool moving;
    [SerializeField] private float explosionTime;
    [SerializeField] private float timeSinceStart;
    [SerializeField] private float rotationSpeed;

    [SerializeField] public UnityEvent OnExplosion;

    private void Update()
    {
        if (!moving)
            return;

        float deltaTime = Time.deltaTime;
        timeSinceStart += deltaTime;

        if (timeSinceStart >= explosionTime)
        {
            Explode();
            return;
        }

        transform.position = Vector3.Lerp(spawn.position, destination.position, timeSinceStart / explosionTime);
        transform.Rotate(Vector3.forward, rotationSpeed * deltaTime);
    }

    public void SetUp(Transform start, Transform _destination, Color color)
    {
        spawn = start;
        destination = _destination;

        foreach (SpriteRenderer sr in graph)
        {
            sr.color = color;
        }
    }

    public void Spawn(float _explosionTime, float _rotationSpeed)
    {
        explosionTime = _explosionTime;
        rotationSpeed = _rotationSpeed;

        moving = true;
        transform.position = spawn.position;
        Show(true);
    }

    public void Explode()
    {
        timeSinceStart = 0;
        moving = false;

        Show(false);
        OnExplosion.Invoke();
        gameObject.SetActive(false);
    }

    private void Show(bool show)
    {
        foreach (SpriteRenderer sr in graph)
        {
            sr.gameObject.SetActive(show);
        }
    }
}
