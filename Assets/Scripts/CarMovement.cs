using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float clamp;

    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        movement *= speed * Time.unscaledDeltaTime;
        transform.position += movement;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clamp, clamp), 0, 0);
    }
}
