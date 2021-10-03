using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float expireTime = 10f;
    [SerializeField] Vector2 direction = Vector2.up;
    float elapsedTime = 0f;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        Vector3 delta = direction * bulletSpeed * Time.deltaTime;
        elapsedTime += Time.deltaTime;
        transform.position += delta;

        if( elapsedTime > expireTime)
        {
            Destroy(this.gameObject);
        }
    }
}
