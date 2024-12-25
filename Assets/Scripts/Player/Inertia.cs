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
        Vector3 boxHalfExtents = new Vector3(0.01f, 0.01f, 0.01f); // �{�b�N�X�̔��a
        Vector3 direction = -transform.up; // �L���X�g�̕���
        float maxDistance = 0.5f; // �L���X�g�̍ő勗��
        Quaternion orientation = transform.rotation; // �{�b�N�X�̉�]

        RaycastHit hit;
        if (Physics.BoxCast(boxCenter, boxHalfExtents, direction, out hit, orientation, maxDistance))
        {
            Debug.Log("BoxCast���q�b�g���܂���: " + hit.collider.name);
        }
        else
        {
            Debug.Log("BoxCast�͉����q�b�g���܂���ł����B");
        }
        Debug.DrawLine(boxCenter, boxCenter + direction * maxDistance, Color.red);
        Debug.DrawRay(boxCenter, boxHalfExtents, Color.green);
        Debug.DrawRay(boxCenter, -boxHalfExtents, Color.green);
        Debug.DrawRay(boxCenter + direction * maxDistance, boxHalfExtents, Color.blue);
        Debug.DrawRay(boxCenter + direction * maxDistance, -boxHalfExtents, Color.blue);
    }
}
