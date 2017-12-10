using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiker : PT_MonoBehaviour {

    public float speed = 5f;
    public string roomXMLString = "{";

    public bool ___________________;

    public Vector3 moveDir;
    public Transform characterTrans;

    void Awake()
    {
        characterTrans = transform.Find("CharacterTrans");
    }

    void Start()
    {
        switch (roomXMLString)
        {
            case "^":
                moveDir = Vector3.up;
                break;
            case "v":
                moveDir = Vector3.down;
                break;
            case "{":
                moveDir = Vector3.left;
                break;
            case "}":
                moveDir = Vector3.right;
                break;
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = moveDir * speed;
    }

    public void Damage(float amt, ElementType eT, bool damageOverTime = false)
    {
        //Nothing damages the spiker
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject go = Utils.FindTaggedParent(other.gameObject);
        if (go == null) return;

        if (go.tag == "Ground")
        {
            float dot = Vector3.Dot(moveDir, go.transform.position - pos);
            if (dot > 0)
            {
                moveDir *= -1;
            }
        }
    }
}
