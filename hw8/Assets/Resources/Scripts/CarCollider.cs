using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{
    private float damage = 0;

    public float GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        damage += 5;
    }
}