using UnityEngine;
using System.Collections;

public class DestroyableScript : MonoBehaviour
{

    public float Health;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log(gameObject.name + " Health: " + Health);
        if (Health < 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
