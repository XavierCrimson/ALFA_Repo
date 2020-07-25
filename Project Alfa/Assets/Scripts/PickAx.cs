﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickAx : MonoBehaviour
{
    [Header ("Pick Ax Throw")]
    [SerializeField] private float throwPower;
    [SerializeField] private float axeRotationSpeed;
    private Rigidbody _pickAxRb;
    private bool _isPickAxInHand;

    [Header("Pick Ax Return")]
    [Tooltip ("Set the distance at which the PickAx is considered back in the hand")]
    [SerializeField] private float grabStep = 0.05f;
    [Tooltip ("lower value = slower lerp")]
    [SerializeField] private float lerpSpeed;
    private Transform _resetTransform;
    void Start()
    {
        _pickAxRb = GameObject.FindGameObjectWithTag("PickAx").gameObject.GetComponent<Rigidbody>();
        Debug.Log(_pickAxRb.gameObject);
        _isPickAxInHand = true;

        _resetTransform = GameObject.FindWithTag("Reset").gameObject.transform;
    }

    void Update()
    {
        bool isAiming = false;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (_isPickAxInHand)
            {
                isAiming = true;
                Debug.Log("Aim");
            }
            Debug.Log(_isPickAxInHand);
        }
        else
        {
            isAiming = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !_isPickAxInHand)
        {
            _pickAxRb.velocity = new Vector3(0,0,0);
            StartCoroutine("PickAxComeBack");
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (_isPickAxInHand)
            {
                if (!isAiming)
                {
                    PickAxHit();
                }
                else
                {
                    PickAxThrow();
                }
            }
            else
            {
                PickAxEffect();
            }
            Debug.Log(_isPickAxInHand);
        }
    }

    private void PickAxHit()
    {
        Debug.Log("Hit");
        //Jouer Animation, définir ce qui est touché par la hache, et lancer la fonction qui contrôle la réaction
    }

    private void PickAxThrow()
    {
        Debug.Log("Throw");
        _isPickAxInHand = false;
        _pickAxRb.transform.parent = null;
        _pickAxRb.isKinematic = false;
        _pickAxRb.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        _pickAxRb.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwPower, ForceMode.Impulse);
        _pickAxRb.AddRelativeTorque(Vector3.right * axeRotationSpeed, ForceMode.Impulse);
    }

    IEnumerator PickAxComeBack()
    {
        while (Vector3.Distance(_resetTransform.position, _pickAxRb.gameObject.transform.position) > grabStep)
        {
            _pickAxRb.gameObject.transform.position = Vector3.Lerp(_pickAxRb.gameObject.transform.position, _resetTransform.position, lerpSpeed);
            _pickAxRb.gameObject.transform.rotation = Quaternion.Lerp(_pickAxRb.gameObject.transform.rotation, _resetTransform.rotation, lerpSpeed);
            yield return null;
        }
        Transform parentHand = GameObject.Find("Main Camera").transform;
        _pickAxRb.gameObject.transform.parent = parentHand;
        _isPickAxInHand = true;
    }

    private void PickAxEffect()
    {

    }
}
