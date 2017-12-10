using System.Collections;
using UnityEngine;

public interface Enemy
{

    Vector3 pos { get; set; }
    float touchDamage { get; set; }
    string typeString { get; set; }

    GameObject gameObject { get; }
    Transform transform { get; }
}
