using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject kunai;
    [SerializeField] GameObject fireSpot;
    [SerializeField] KunaiManager kunaiManager;

    public float fireTime;
    public float atkSpeed;

    void Start()
    {
        StartCoroutine(fire());
    }

    IEnumerator fire()
    {
        //Calculates direction for kunai
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection.z = 0f;
        shootDirection -= transform.position;
        shootDirection.Normalize();
        //Throws the kunai
        GameObject bullet = Instantiate(kunai, fireSpot.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * kunaiManager.Speed;
        bullet.transform.right = shootDirection;
        Destroy(bullet, 3);
        yield return new WaitForSeconds(fireTime/(atkSpeed + 1));
        StartCoroutine(fire());
    }
}
