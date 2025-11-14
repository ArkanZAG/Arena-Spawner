using System;
using Unity.Mathematics;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] private Vector2 turn;
    [SerializeField] private float cameraSensitivity;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * cameraSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * cameraSensitivity;
        transform.rotation = quaternion.Euler(-turn.y, turn.x, 0);
    }
}