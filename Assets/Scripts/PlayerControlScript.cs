using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour
{
    private Animator _animator;
    
    public float MoveSpeed;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
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
    }


    private void FixedUpdate()
    {
        ApplyControlInput();
    }

    private void ApplyControlInput()
    {
        ApplyLineOfSight();
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        var vertical = Input.GetAxis("Vertical");
        rigidbody2D.AddForce(Vector2.up * MoveSpeed * vertical);
        
        var horizontal = Input.GetAxis("Horizontal");
        rigidbody2D.AddForce(Vector2.right * MoveSpeed * horizontal);

        var walking = horizontal != 0 || vertical != 0;
        _animator.SetBool("Walking", walking);
    }

    private void ApplyLineOfSight()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rigidbody2D.angularVelocity = 0;
    }
}
