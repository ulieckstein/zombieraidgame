using UnityEngine;
using System.Collections;

public class DestroyableScript : MonoBehaviour
{

    public float InitialHealth;
    private float _health;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log(gameObject.name + " Health: " + _health);
        if (_health < 0) Die();
    }

    public float GetHealthPercentage()
    {
        return _health/InitialHealth;
    }

    public void Die()
    {
        _animator.SetBool("Destroyed", true);
        _animator.SetFloat("HealthPercent", GetHealthPercentage());
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
