using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    public float HitPointsMax;
    public float HitPointsMin;
    public int PrecisionPercent;
    
    public float PushBackForce;
    public float PushBackDelay;

    private float _currentDamage;
    private float _hitAccuracy;

    public GameObject HitAnimation;
    private Animator HitAnimationController;

    void Start()
    {
        _currentDamage = 0;
        _hitAccuracy = 0.0f;
        if (HitAnimation != null) HitAnimationController = HitAnimation.GetComponent<Animator>();
    }

    private void SetAttackValues()
    {
        _hitAccuracy = (Random.Range(0, 100) < PrecisionPercent ? 1 : 0);
        _currentDamage = Random.Range(HitPointsMax, HitPointsMin)*_hitAccuracy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SetAttackValues();
        //Debug.Log("Hit " + other.name + " with damage " + _currentDamage);
        var destroyable = other.gameObject.GetComponent<DestroyableScript>();
        if (destroyable != null)
        {
            destroyable.TakeDamage(_currentDamage);
            StartCoroutine(PushBack(other.gameObject));
            TriggerHitAnimation(other);
        }
    }

    private void TriggerHitAnimation(Collider2D other)
    {
        if (HitAnimation == null) return;
        HitAnimation.transform.position = other.transform.position ; 
        HitAnimationController.SetFloat("Damage", _currentDamage);
        HitAnimationController.SetTrigger("Hit");
    }

    IEnumerator PushBack(GameObject other)
    {
        yield return new WaitForSeconds(PushBackDelay);
        if (other != null)
            other.rigidbody2D.AddForce(gameObject.transform.up * PushBackForce * _hitAccuracy);
    }
}
