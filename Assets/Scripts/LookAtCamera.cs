using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        if(Player.Instance == null) return;
        
        transform.LookAt(Player.Instance._Camera.transform.position);
        transform.Rotate(0, 180, 0);
    }
}
