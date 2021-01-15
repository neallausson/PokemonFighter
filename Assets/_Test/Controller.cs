using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Controller : MonoBehaviour {

    public abstract void ReadInput(InputData data);

    protected Rigidbody rb;
    protected Collider coll;
    protected bool newInput;

    [SerializeField]
    protected float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

}
