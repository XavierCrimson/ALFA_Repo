using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickAx : MonoBehaviour
{
    private Rigidbody _pickAx;
    private bool _isPickAxInHand;
    void Start()
    {
        _pickAx = GameObject.FindGameObjectWithTag("PickAx").gameObject.GetComponent<Rigidbody>();
        Debug.Log(_pickAx.gameObject);
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
    }

    private void PickAxComeBack()
    {
        _isPickAxInHand = true;
    }

    private void PickAxEffect()
    {

    }
}
