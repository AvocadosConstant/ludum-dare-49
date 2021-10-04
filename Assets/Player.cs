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
    [SerializeField] float cooldown = 0.05f;
    [SerializeField] PlayerMode mode = PlayerMode.Mode1;

    Vector2 moveDirection;
    Vector2 mouseLoc;

    bool clickPressed;
    bool fireReady = true;
    bool down_col = false;
    bool up_col = false;
    bool left_col = false;
    bool right_col = false;
    float timePressed = 0f;

    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    
    void Update()
    {
        mouseLoc = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        ProcessMovement();
        ProcessFire();
        
    }

    void ProcessMovement()
    {
        Vector3 delta = moveDirection * moveSpeed * Time.deltaTime;

        if (down_col && moveDirection.y > 0 || up_col && moveDirection.y < 0)
        {
            delta.y = 0;
        }
        if (left_col && moveDirection.x > 0 || right_col && moveDirection.x < 0)
        {
            delta.x = 0;
        }

        transform.position += delta;
    }

    void ProcessFire()
    {
        if (mode == PlayerMode.Mode2 && clickPressed)
        {
            Mode2FireAux();
        }
    }
    
    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
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
        Vector3 spawnLoc = transform.position;
        Vector2 direction = new Vector2(mouseLoc.x - spawnLoc.x, mouseLoc.y - spawnLoc.y).normalized;

        Bullet bulletInstance = Instantiate(bulletPrefab, spawnLoc, Quaternion.identity).GetComponent<Bullet>();
        bulletInstance.SetDirection(direction);
    }

    void LaunchMissile(float timeHeld)
    {
        Vector3 spawnLoc = transform.position;
        Vector2 direction = new Vector2(mouseLoc.x - spawnLoc.x, mouseLoc.y - spawnLoc.y).normalized;
        spawnLoc.x += direction.x * timeHeld;
        spawnLoc.y += direction.y * timeHeld;
        spawnLoc.z = 1;
        Instantiate(missilePrefab, spawnLoc, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name.Equals("down_col"))
        {
            down_col = true;
        }
        if (col.gameObject.name.Equals("up_col"))
        {
            up_col = true;
        }
        if (col.gameObject.name.Equals("left_col"))
        {
            left_col = true;
        }
        if (col.gameObject.name.Equals("right_col"))
        {
            right_col = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "down_col")
        {
            down_col = false;
        }
        if (col.gameObject.name == "up_col")
        {
            up_col = false;
        }
        if (col.gameObject.name == "left_col")
        {
            left_col = false;
        }
        if (col.gameObject.name == "right_col")
        {
            right_col = false;
        }
    }
}
