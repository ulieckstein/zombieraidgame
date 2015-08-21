using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public float MoveSpeed;
    private float _initialMoveSpeed;
    private float _strafeSpeed;
    public Transform Prey;
    private Animator _animator;
    private bool _alive;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _alive = true;
        if (Prey == null) Prey = GameObject.FindGameObjectWithTag("Player").transform;
        _initialMoveSpeed = MoveSpeed;
        _strafeSpeed = MoveSpeed/4;
    }

    private void Update()
    {
        _alive = _animator.GetFloat("HealthPercent") > 0;
    }

    private void FixedUpdate()
    {
        if (_alive)
        {
            FollowPrey();
        }
        else
        {
            gameObject.rigidbody2D.isKinematic = true;
            gameObject.isStatic = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") _animator.SetBool("PlayerContact", true);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            AvoidObstacle();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") _animator.SetBool("PlayerContact", false);
        MoveSpeed = _initialMoveSpeed;
    }

    private void FollowPrey()
    {
        float z = Mathf.Atan2((Prey.transform.position.y - transform.position.y),
                (Prey.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        rigidbody2D.AddForce(gameObject.transform.up * MoveSpeed);
    }

    private void AvoidObstacle()
    {
        const float raycastDistance = 1f;
        var rayHits = Physics2D.RaycastAll(transform.position, transform.up, raycastDistance);
        var colliderHit = rayHits.FirstOrDefault(h => h.collider.tag == "Enemy" && h.collider != gameObject.collider2D);
        if (colliderHit.collider != null)
        {
            var direction = Physics2D.Raycast(transform.position, transform.right, raycastDistance)
                ? transform.right
                : -transform.right;
            Debug.Log(colliderHit.collider.name + " is in front of " + gameObject.name);
            gameObject.rigidbody2D.AddForce(direction * _strafeSpeed);
            MoveSpeed = 0;
        }
    }
}
