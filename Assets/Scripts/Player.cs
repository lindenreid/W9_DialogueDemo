using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 1.0f;
    [SerializeField] private float _turnSpeed = 1.0f;
    public Camera _Camera;

    public static Player Instance { get; private set; } // singleton stuff

    // singleton stuff
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // movement
    private void Update ()
    {
        float translation = Input.GetAxis("Vertical") * _forwardSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}