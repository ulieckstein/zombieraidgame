using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour
{
    private Animator _animator;
    private WeaponScript _weapon;
    
    public float MoveSpeed;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<WeaponScript>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        _weapon.Attack();
    }


    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rigidbody2D.angularVelocity = 0;

        var vertical = Input.GetAxis("Vertical");
        rigidbody2D.AddForce(gameObject.transform.up * MoveSpeed * vertical);
        _animator.SetBool("Walking", vertical != 0);
    }
}
