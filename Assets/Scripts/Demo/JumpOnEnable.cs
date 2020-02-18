using System;
using UnityEngine;

public class JumpOnEnable : MonoBehaviour
{
    [SerializeField] private float jumpStrength;

    private Rigidbody _cachedRigidbody;
    private Rigidbody Rigidbody => _cachedRigidbody ? _cachedRigidbody : _cachedRigidbody = GetComponent<Rigidbody>();

    protected void OnEnable()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.AddForce(Vector3.up * jumpStrength);
    }
}
