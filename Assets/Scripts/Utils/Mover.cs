using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10.0f;

    [SerializeField]
    float disableDelay = 1.0f;

    float disableTimer;

    Vector2 moveDirection = Vector2.right;
    Vector3 velocity;

    Rigidbody2D rigid;

    bool isDisable = false;

    void Awake()
    {
        Initialize();
    }

    void OnEnable()
    {
        disableTimer = Time.time + disableDelay;
        isDisable = false;
    }

    void Update()
    {
        DisableHandler();
    }

    void FixedUpdate()
    {
        MoveHandler();
    }

    void Initialize()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;
    }

    void MoveHandler()
    {
        velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + velocity);
    }

    void DisableHandler()
    {
        if (isDisable)
            return;

        if (disableTimer < Time.time)
        {
            isDisable = true;
            gameObject.SetActive(false);
        }
    }

    public void Move(Vector2 direction)
    {
        moveDirection = direction;
    }
}

