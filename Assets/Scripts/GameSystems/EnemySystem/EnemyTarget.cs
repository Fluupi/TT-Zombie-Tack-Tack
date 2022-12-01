using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyTarget : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Enemy explosive = collision.gameObject.GetComponent<Enemy>();
        if (explosive != null)
            explosive.Explode();
    }
}
