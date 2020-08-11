using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAx_Effect : MonoBehaviour
{
    private Rigidbody _pickAxRb;
    void Start()
    {
        _pickAxRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Player")
        {
            
            _pickAxRb.isKinematic = true;
        }
    }
}
