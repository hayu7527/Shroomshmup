using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChild : MonoBehaviour
{
    private BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        if (bndCheck.offRight)
        {
            Destroy(gameObject);
        }

        if (bndCheck.offLeft)
        {
            Destroy(gameObject);
        }
    }
}
