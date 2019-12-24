using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    const int MAX_BULLET_POOL = 20;

    [SerializeField]
    float shootRate = 0.5f;

    [SerializeField]
    Transform barrel;

    [SerializeField]
    GameObject bulletPrefab;

    float triggerTimer;
    Mover[] bullets;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        bullets = new Mover[MAX_BULLET_POOL];

        for (int i = 0; i < MAX_BULLET_POOL; ++i)
        {
            bullets[i] = Instantiate(bulletPrefab).GetComponent<Mover>();
            bullets[i].gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector2 direction)
    {
        bool isAllowToShoot = triggerTimer < Time.time;

        if (!isAllowToShoot)
            return;

        triggerTimer = Time.time + shootRate;

        for (int i = 0; i < MAX_BULLET_POOL; ++i)
        {
            if (bullets[i].gameObject.activeSelf)
                continue;

            bullets[i].transform.position = barrel.position;
            bullets[i].Move(direction);
            bullets[i].gameObject.SetActive(true);

            break;
        }
    }
}

