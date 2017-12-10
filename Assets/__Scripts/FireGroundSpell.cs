using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGroundSpell : PT_MonoBehaviour {

    public float duration = 4;
    public float durationVariance = 0.5f;

    public float fadeTime = 1f;
    public float timeStart;
    public float damagePerSecond = 10;

    void Start()
    {
        timeStart = Time.time;
        duration = Random.Range(duration - durationVariance, duration + durationVariance);
    }

    void Update()
    {
        float u = (Time.time - timeStart) / duration;
        float fadePercent = 1 - (fadeTime / duration);

        if (u > fadePercent)
        {
            float u2 = (u - fadePercent) / (1 - fadePercent);
            Vector3 loc = pos;
            loc.z = u2 * 2;
            pos = loc;
        }

        if (u > 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject go = Utils.FindTaggedParent(other.gameObject);

        if (go == null)
        {
            go = other.gameObject;
        }

        Utils.tr("Flame hit", go.name);
    }

    void OnTriggerStay(Collider other)
    {
        EnemyBug recipient = other.GetComponent<EnemyBug>();

        if (recipient != null)
        {
            recipient.Damage(damagePerSecond, true);
        }
    }

}
