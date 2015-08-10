using UnityEngine;
using System.Collections;

public class PlayerControlScript : MonoBehaviour
{
    public float moveSpeed;

    void FixedUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        rigidbody2D.angularVelocity = 0;

        rigidbody2D.AddForce(gameObject.transform.up * moveSpeed * Input.GetAxis("Vertical"));
    }
}
