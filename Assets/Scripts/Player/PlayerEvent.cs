using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerEvent : MonoBehaviour
{
    Vector3 velocity;
    Vector3 playerAngle;
    public void Move(InputAction.CallbackContext context)
    {
        var axis = context.ReadValue<Vector2>();
        velocity = new Vector3(axis.x, 0, axis.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        float normalizedDir = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, playerAngle.y, 0.0f);
        playerAngle.y = normalizedDir;
    }
}
