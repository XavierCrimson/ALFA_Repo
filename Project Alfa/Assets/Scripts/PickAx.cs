using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickAx : MonoBehaviour
{
    private Rigidbody pickAx;
    private bool isPickAxInHand;
    void Start()
    {
        pickAx = GameObject.FindGameObjectWithTag("PickAx").gameObject.GetComponent<Rigidbody>();
        Debug.Log(pickAx.gameObject);
        isPickAxInHand = true;
    }

    void Update()
    {
        bool isAiming = false;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (isPickAxInHand)
            {
                isAiming = true;
                Debug.Log("Aim");
            }
            else
            {
                PickAxComeBack();
            }
            Debug.Log(isPickAxInHand);
        }
        else
        {
            isAiming = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isPickAxInHand)
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
            Debug.Log(isPickAxInHand);
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
        isPickAxInHand = false;
    }

    private void PickAxComeBack()
    {
        isPickAxInHand = true;
    }

    private void PickAxEffect()
    {

    }
}
