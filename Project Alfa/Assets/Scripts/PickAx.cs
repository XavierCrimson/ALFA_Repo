using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickAx : MonoBehaviour
{
    [SerializeField] private float throwPower;
    [SerializeField] private float axeRotationSpeed;
    private Rigidbody _pickAxRb;
    private bool _isPickAxInHand;
    void Start()
    {
        _pickAxRb = GameObject.FindGameObjectWithTag("PickAx").gameObject.GetComponent<Rigidbody>();
        Debug.Log(_pickAxRb.gameObject);
        _isPickAxInHand = true;
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
            else
            {
                PickAxComeBack();
            }
            Debug.Log(_isPickAxInHand);
        }
        else
        {
            isAiming = false;
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

    private void PickAxComeBack()
    {
        _isPickAxInHand = true;
    }

    private void PickAxEffect()
    {

    }
}
