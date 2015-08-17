using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform player;
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y),
            (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        rigidbody2D.AddForce(gameObject.transform.up * moveSpeed);
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

}
