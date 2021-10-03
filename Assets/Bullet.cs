using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float expireTime = 10f;
    float elapsedTime = 0f;


    void Update()
    {
        Vector3 delta = Vector2.up * bulletSpeed * Time.deltaTime;
        elapsedTime += Time.deltaTime;
        transform.position += delta;

        if( elapsedTime > expireTime)
        {
            Destroy(this.gameObject);
        }
    }
}
