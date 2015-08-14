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
    
    void Start()
    {
        _currentDamage = 0;
        _hitAccuracy = 0.0f;
    }

    public void Attack()
    {
        _hitAccuracy = (Random.Range(0, 100) < PrecisionPercent ? 1 : 0);
        _currentDamage = Random.Range(HitPointsMax, HitPointsMin)*_hitAccuracy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit something with damage " + _currentDamage);
        if (other.gameObject.tag == "Enemy")
        {
            var destroyable = other.gameObject.GetComponent<DestroyableScript>();
            destroyable.TakeDamage(_currentDamage);
            StartCoroutine(PushBack(other.gameObject));
        }
    }

    IEnumerator PushBack(GameObject other)
    {
        yield return new WaitForSeconds(PushBackDelay);
        if (other != null)
            other.rigidbody2D.AddForce(gameObject.transform.up * PushBackForce * _hitAccuracy);
    }
}
