using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isAttackingHash;
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash= Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPress = Input.GetKey(KeyCode.W);
        bool backwardPress = Input.GetKey(KeyCode.S);
        bool leftPress = Input.GetKey(KeyCode.A);
        bool rightPress = Input.GetKey(KeyCode.D);
        bool runPress = Input.GetKey(KeyCode.LeftShift);
        bool isAttacking=animator.GetBool(isAttackingHash);

        // Set isWalking based on whether the player is pressing any movement keys.
        animator.SetBool(isWalkingHash, forwardPress || backwardPress || leftPress || rightPress);

        // Set isRunning based on whether the player is pressing the run key.
        animator.SetBool(isRunningHash, runPress);

if(Input.GetMouseButtonDown(0))
{
    if(!isAttacking)
        animator.SetBool(isAttackingHash, true);
}
if (Input.GetMouseButtonUp(0))
    animator.SetBool(isAttackingHash, false);
        // Calculate the movement direction based on the WASD keys.
        Vector3 movement = Vector3.zero;
        if (forwardPress)
        {
            movement += transform.forward;
        }
        if (backwardPress)
        {
            movement -= transform.forward;
        }
        if (leftPress)
        {
            movement -= transform.right;
        }
        if (rightPress)
        {
            movement += transform.right;
        }

        // Normalize the movement vector to prevent diagonal movement from being faster.
        if (movement != Vector3.zero)
        {
            movement.Normalize();

            // Move the player based on the movement direction and speed.
            float speed = isRunning ? 10f : 5f;
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        // Rotate the player based on the movement keys that are pressed.
        Vector3 rotation = Vector3.zero;
        if (leftPress)
        {
            rotation = Vector3.down;
        }
        if (rightPress)
        {
            rotation = Vector3.up;
        }

        // Rotate the player using the transform.
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
