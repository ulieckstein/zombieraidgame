using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform player;
    private Animator _animator;
    private bool _alive;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _alive = true;
    }

    private void Update()
    {
        _alive = _animator.GetFloat("HealthPercent") > 0;
    }

    private void FixedUpdate()
    {
        if (_alive)
        {
            FollowPlayer();
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
        Debug.Log("Collided with " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") _animator.SetBool("PlayerContact", true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Lost Contact with " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") _animator.SetBool("PlayerContact", false);
    }

    private void FollowPlayer()
    {
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y),
                (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        rigidbody2D.AddForce(gameObject.transform.up * moveSpeed);
    }
}
