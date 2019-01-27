using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : MonoBehaviour {

    public float speed = 5f;
    public float speedIncreaseAmount = 1;
    public float speedDecreaseAmount = 2f;
    public float maxSpeed = 25f;
    bool keyAlternate = false;

    [Space, Header("Jump")]
    public float jumpSpeed;
    public float forwardJumpSpeed;
    public float gravity;
    public float gravityMultiplier = 2;
    public Vector3 boxCastHalfExtents;
    public LayerMask groundLayer;

    [Space, Header("Animation")]
    public Animator animator;
    public float animationSmoothTime = .5f;

    [Space, Header("Sounds")]
    public List<AudioClip> footsteps;
    public List<AudioClip> jumps;

    AudioSource source;

    float initialGravity;
    float verticalVelocity;
    bool isGrounded;
    bool isFalling;

    private void Start()
    {
        initialGravity = gravity;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = Grounded();

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && keyAlternate == false)
        {
            source.PlayOneShot(Utilities.GetRandomItem(footsteps));

            speedIncreaseAmount *= 2.5f;
            speed += speedIncreaseAmount;
            keyAlternate = true;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && keyAlternate == true)
        {
            source.PlayOneShot(Utilities.GetRandomItem(footsteps));
            speedIncreaseAmount *= 2.5f;
            speed += speedIncreaseAmount;
            keyAlternate = false;
        }

        speedIncreaseAmount = Mathf.Clamp(speedIncreaseAmount, 1, 15);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))&& isGrounded)
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && !isFalling)
        {
            isFalling = true;
            gravity *= gravityMultiplier;
        }

        speedIncreaseAmount = Mathf.Lerp(speedIncreaseAmount, 1, speedDecreaseAmount * Time.unscaledDeltaTime);
        if (isGrounded)
            speed = Mathf.Lerp(speed, 0, 5f * Time.unscaledDeltaTime);


        speed = Mathf.Clamp(speed, 0, maxSpeed);
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, speed * Time.unscaledDeltaTime);
        transform.position += new Vector3(0, verticalVelocity * Time.unscaledDeltaTime, 0) + transform.forward * forwardJumpSpeed * Time.unscaledDeltaTime;
        Fall();

        animator.SetFloat("Speed", speed, animationSmoothTime, Time.unscaledDeltaTime);
    }

    void Jump()
    {
        source.PlayOneShot(Utilities.GetRandomItem(jumps));
        verticalVelocity = jumpSpeed;
    }

    void Fall()
    {
        verticalVelocity -= gravity * Time.unscaledDeltaTime;

        if (verticalVelocity < 0 && isGrounded)
        {
            verticalVelocity = 0;
            gravity = initialGravity;
            isFalling = false;
        }
    }



    bool Grounded()
    {
        return Physics.OverlapBox(transform.position, boxCastHalfExtents, Quaternion.identity, groundLayer).Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, boxCastHalfExtents * 2);
    }
}