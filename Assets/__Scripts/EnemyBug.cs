using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBug : PT_MonoBehaviour {
    public float speed = 0.5f;

    public bool ________________;

    public Vector3 walkTarget;
    public bool walking;
    public Transform characterTrans;

    void Awake()
    {
        characterTrans = transform.Find("CharacterTrans");
    }

    void Update()
    {
        WalkTo(Mage.S.pos);
    }

    // ---------------- Walking Code ----------------

    public void WalkTo(Vector3 xTarget)
    {
        walkTarget = xTarget;
        walkTarget.z = 0;
        walking = true;
        Face(walkTarget);
    }

    public void Face(Vector3 poi)
    {
        Vector3 delta = poi - pos;
        float rZ = Mathf.Rad2Deg * Mathf.Atan2(delta.y, delta.x);
        characterTrans.rotation = Quaternion.Euler(0, 0, rZ);
    }

    public void StopWalking()
    {
        walking = false;
        rigidbody.velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (walking)
        {
            if ((walkTarget - pos).magnitude < speed * Time.fixedDeltaTime)
            {
                pos = walkTarget;
                StopWalking();
            }
            else
            {
                rigidbody.velocity = (walkTarget - pos).normalized * speed;
            }
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }

    }
}
