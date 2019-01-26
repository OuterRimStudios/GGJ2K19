using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed;
    public float clamp;
    [Range(0, 1)]
    public float inputDelay = 0.35f;
    float xMove;

    private void Update()
    {
        xMove = Mathf.Lerp(xMove, Input.GetAxisRaw("Horizontal"), inputDelay);
        Vector3 movement = new Vector3(xMove, 0, 0);
        movement *= speed * Time.unscaledDeltaTime;
        transform.position += movement;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clamp, clamp), 0, 0);
    }
}
