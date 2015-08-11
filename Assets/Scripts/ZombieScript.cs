using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform player;

    private void FixedUpdate()
    {
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y),
            (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        rigidbody2D.AddForce(gameObject.transform.up * moveSpeed);
    }

}
