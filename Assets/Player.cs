using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    enum PlayerMode
    {
        Mode1,
        Mode2,
        Mode3
    }

    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] float maxHold = 3f;
    [SerializeField] float cooldown = 0.5f;
    [SerializeField] PlayerMode mode = PlayerMode.Mode1;

    Vector2 moveDirection;
    Vector2 mouseLoc;

    bool clickPressed;
    bool fireReady = true;
    float timePressed = 0f;
    

    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    

    void Update()
    {
        Vector3 delta = moveDirection * moveSpeed * Time.deltaTime;
        transform.position += delta;

        if (mode == PlayerMode.Mode2 && clickPressed)
        {
            Mode2FireAux();
        }
    }
    
    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        mouseLoc = value.Get<Vector2>();
        Debug.Log(mouseLoc);
    }

    void OnFire(InputValue value)
    {
        switch(mode)
        {
            case PlayerMode.Mode1:
            {
                Mode1Fire(value);
                break;
            }
            case PlayerMode.Mode2:
            {
                Mode2Fire(value);
                break;
            }
            case PlayerMode.Mode3:
            {
                Mode3Fire(value);
                break;
            }
        }
    }

    void OnSwitch(InputValue value)
    {
        mode = (PlayerMode) (((int) mode + 1) % 3);
    }

    void Mode1Fire(InputValue value)
    {
        clickPressed = value.isPressed;
        if (clickPressed)
        {
            LaunchProjectile();
        }
    }

    void Mode2Fire(InputValue value)
    {
        clickPressed = value.isPressed;
        if (clickPressed)
        {
            timePressed = Time.time;
        }
    }

    void Mode2FireAux()
    {
        if (fireReady)
        {
            fireReady = false;
            LaunchProjectile();
        }
        else if (Time.time - timePressed > cooldown)
        {
            fireReady = true;
            timePressed = Time.time;
        }
    }

    void Mode3Fire(InputValue value)
    {
        clickPressed = value.isPressed;
        if (clickPressed)
        {
            timePressed = Time.time;
        }
        else if (!clickPressed)
        {
            float timeHeld = Mathf.Min(Time.time - timePressed, maxHold);
            LaunchMissile(timeHeld);
        }
    }

    void LaunchProjectile()
    {
        Vector3 center = transform.position;
        Instantiate(bulletPrefab, center, Quaternion.identity);
    }

    void LaunchMissile(float timeHeld)
    {
        Vector3 spawnLoc = transform.position;
        spawnLoc.z = 1;
        spawnLoc.y += timeHeld;
        Instantiate(missilePrefab, spawnLoc, Quaternion.identity);
    }
}