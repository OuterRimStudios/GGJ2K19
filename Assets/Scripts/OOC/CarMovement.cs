using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public static CarMovement Instance;
    public Transform stopLocation;
    public float autoMoveSpeed;
    public Animator wheelAnimator;
    public Animator handAnimator;
    public float grabSpeed;
    public Animator fadeAnimator;

    public float speed;
    public float clamp;
    [Range(0, 1)]
    public float inputDelay = 0.35f;
    float xMove;
    bool stopMove;
    bool arrived;
    bool done;

    float fakeDistance;

    private void Awake()
    {
        Instance = this;
        fakeDistance = 100;
        handAnimator.SetFloat("Distance", fakeDistance);
    }

    private void Update()
    {
        if (arrived && OutOfControlManager.Instance.activeEnemies.Count <= 0)
            stopMove = true;

        if(!stopMove)
            Move();
        else
        {
            transform.position = Vector3.Lerp(transform.position, stopLocation.position, autoMoveSpeed * Time.unscaledDeltaTime);
            if(Utilities.CheckDistance(transform.position, stopLocation.position) < 14f && !done)
            {
                done = true;
                StartCoroutine(Grab());
            }

        }
    }

    IEnumerator Grab()
    {
        yield return new WaitUntil(WaitForGrab);
        print("Grabbed");
        GameManager.Instance.Transition();
    }

    bool WaitForGrab()
    {
        fakeDistance -= grabSpeed * Time.deltaTime;
        print(fakeDistance);
        handAnimator.SetFloat("Distance", fakeDistance);
        fadeAnimator.SetTrigger("Fade");
        if (fakeDistance <= 0)
            return true;
        else return false;
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
