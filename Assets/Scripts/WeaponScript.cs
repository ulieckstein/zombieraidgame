using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{

    private Animator _animator;
    public float HitPoints;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
        }
    }
}
