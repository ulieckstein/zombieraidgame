using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public float MoveSpeed;
    private float _initialMoveSpeed;
    private float _strafeSpeed;
    public Vector3 Prey;
    private Animator _animator;
    private bool _alive;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _alive = true;
        _initialMoveSpeed = MoveSpeed;
        _strafeSpeed = MoveSpeed/4;
        FocusPlayer();
    }

    private void FocusPlayer()
    {
        Prey = GameObject.FindGameObjectWithTag("Player").transform.position;
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
        else CreateCircularRaycast();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            //AvoidObstacle();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") _animator.SetBool("PlayerContact", false);
        MoveSpeed = _initialMoveSpeed;
    }

    private void FollowPrey()
    {
        float z = Mathf.Atan2((Prey.y - transform.position.y),
                (Prey.x - transform.position.x)) * Mathf.Rad2Deg - 90;
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

    private void CreateCircularRaycast()
    {
        const float raycastDistance = 3f;
        var startingVector = new Vector2(transform.up.x, transform.up.y);
        // rotate vector for raycast
        for (var i = -100; i <= 260; i += 10)
        {
            var direction = Rotate(startingVector, i);
            var rayHits = Physics2D.RaycastAll(transform.position, direction, raycastDistance);
            if (rayHits.All(h => h.collider == gameObject.collider2D || h.collider.tag == "Player" || h.collider.tag == "Weapon"))
            {
                Debug.Log("zombie is at " + transform.position.x + " " + transform.position.y);
                Debug.Log("zombie will now follow " + direction.x + " " + direction.y);
                Prey = direction;
            }
        }

    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}
