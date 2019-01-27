using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public static CarMovement Instance;
    public Transform stopLocation;
    public float autoMoveSpeed;
    public Animator wheelAnimator;

    public float speed;
    public float clamp;
    [Range(0, 1)]
    public float inputDelay = 0.35f;
    float xMove;
    bool stopMove;
    bool arrived;
    bool done;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (arrived && OutOfControlManager.Instance.activeEnemies.Count <= 0)
            stopMove = true;

        if(!stopMove)
            Move();
        else
        {
            if (Utilities.CheckDistance(transform.position, stopLocation.position) > .1f)
                transform.position = Vector3.Lerp(transform.position, stopLocation.position, autoMoveSpeed * Time.unscaledDeltaTime);
            else if(!done)
            {
                done = true;
                GameManager.Instance.Transition();
            }

        }
    }

    private void Move()
    {
        xMove = Mathf.Lerp(xMove, Input.GetAxisRaw("Horizontal"), inputDelay);
        wheelAnimator.SetFloat("TurnAngle", xMove);
        Vector3 movement = new Vector3(xMove, 0, 0);
        movement *= speed * Time.unscaledDeltaTime;
        transform.position += movement;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clamp, clamp), 0, 0);
    }

    public void Arrived()
    {
        arrived = true;
    }
}
