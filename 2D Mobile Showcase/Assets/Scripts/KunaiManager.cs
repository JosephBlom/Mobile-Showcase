using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class KunaiManager : MonoBehaviour
{
    public float baseDamage;
    public float baseSpeed;
    public float damageMultiplier = 1;
    public float speedMultiplier = 1;
    public float Damage;
    public float Speed;

    void Start()
    {
        
    }

    void Update()
    {
        Damage = baseDamage * damageMultiplier;
        Speed = baseSpeed * speedMultiplier;
    }
}
