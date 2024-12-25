using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertia : MonoBehaviour
{
    private PlayerController player;
    private CharacterController controller;
    private BlockController platform;
    private Vector3 platformVelocity;
    //[SerializeField] BoxCollider boxCollider;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        Vector3 boxCenter = transform.position;
        Vector3 boxHalfExtents = new Vector3(0.01f, 0.01f, 0.01f); // ボックスの半径
        Vector3 direction = -transform.up; // キャストの方向
        float maxDistance = 0.5f; // キャストの最大距離
        Quaternion orientation = transform.rotation; // ボックスの回転

        RaycastHit hit;
        if (Physics.BoxCast(boxCenter, boxHalfExtents, direction, out hit, orientation, maxDistance))
        {
            Debug.Log("BoxCastがヒットしました: " + hit.collider.name);
        }
        else
        {
            Debug.Log("BoxCastは何もヒットしませんでした。");
        }
        Debug.DrawLine(boxCenter, boxCenter + direction * maxDistance, Color.red);
        Debug.DrawRay(boxCenter, boxHalfExtents, Color.green);
        Debug.DrawRay(boxCenter, -boxHalfExtents, Color.green);
        Debug.DrawRay(boxCenter + direction * maxDistance, boxHalfExtents, Color.blue);
        Debug.DrawRay(boxCenter + direction * maxDistance, -boxHalfExtents, Color.blue);
    }
}
