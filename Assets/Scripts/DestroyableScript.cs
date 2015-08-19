using System;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class DestroyableScript : MonoBehaviour
{

    public float InitialHealth;
    private float _health;
    private bool _destroyed;
    
    private Animator _animator;
    public Sprite DestroyedSprite;
    public float DestroyAnimationSeconds;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _health = InitialHealth;
        _destroyed = false;
    }

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            Debug.Log(gameObject.name + " Health: " + _health);
            _animator.SetFloat("HealthPercent", GetHealthPercentage());
            if (_health <= 0) Die();
        }
    }

    public float GetHealthPercentage()
    {
        return _health/InitialHealth;
    }

    public void Die()
    {
        if (!_destroyed)
        {
            _destroyed = true;
            _animator.SetBool("Destroyed", _destroyed);
            StartCoroutine(ReplaceWithDestroyedSprite());
        }
    }

    private IEnumerator ReplaceWithDestroyedSprite()
    {
        yield return new WaitForSeconds(DestroyAnimationSeconds);
        if (DestroyedSprite != null)
        {
            var remains = new GameObject("Destroyed " + gameObject.name);
            remains.transform.position = gameObject.transform.position;
            remains.transform.rotation = gameObject.transform.rotation;
            var remainsSprite = remains.AddComponent<SpriteRenderer>();
            remainsSprite.sprite = DestroyedSprite;
            remainsSprite.sortingLayerID = 1;
        }
        Destroy(gameObject);
    }
}
