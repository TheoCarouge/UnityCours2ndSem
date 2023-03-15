using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] InputsReceiver _inputsReceiver;
    public Vector2 lookInput;

    private void Update()
    {
        LookGamePad();
    }

    private void LookGamePad()
    {
        if (_inputsReceiver.IsUsingGamepad)
        {
            Vector3 direction2 = Vector3.up * lookInput.y + Vector3.right * lookInput.x;
            //le player regarde dans la direction de son mouvement si aucun input
            if (Mathf.Abs(lookInput.x) > 0.1f || Mathf.Abs(lookInput.y) > 0.1f)
            {
                gun.transform.right = direction2;
            }
        }
    }
}