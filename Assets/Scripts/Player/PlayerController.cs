using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform aimSight;

    [SerializeField]
    Transform barrel;

    [SerializeField]
    float moveSpeed = 5.0f;

    [SerializeField]
    GunController gun;

    bool isFacingRight = true;

    Vector2 inputVector;
    Vector2 moveDirection;
    Vector2 velocity;
    Vector2 shootDirection;

    Rigidbody2D rigid;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        InputHandler();
    }

    void LateUpdate()
    {
        AimHandler();
        FlipHandler();
    }

    void FixedUpdate()
    {
        MoveHandler();
    }

    void Initialize()
    {
        rigid = GetComponent<Rigidbody2D>();
        aimSight.parent = null;
    }

    void InputHandler()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot(shootDirection);
        }
    }

    void MoveHandler()
    {
        moveDirection = inputVector;

        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection = moveDirection.normalized;
        }

        velocity = moveDirection * moveSpeed;
        rigid.velocity = velocity * Time.fixedDeltaTime;
    }

    void AimHandler()
    {
        aimSight.position = transform.position;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 relativeVector = mousePosition - transform.position;

        float angle = Mathf.Atan2(relativeVector.y, relativeVector.x) * Mathf.Rad2Deg;

        aimSight.rotation = Quaternion.Euler(0, 0, angle);
        shootDirection = barrel.TransformDirection(Vector3.up);
    }

    void FlipHandler()
    {
        if (inputVector.x > 0.0f && !isFacingRight)
        {
            FlipX();
        }
        else if (inputVector.x < 0.0f && isFacingRight)
        {
            FlipX();
        }
    }

    void FlipX()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;

        newScale.x *= -1.0f;
        transform.localScale = newScale;
    }
}
