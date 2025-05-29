using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public ScreenFader FadeCode;
    private void OnTriggerEnter(Collider other)
    {
        FadeCode.FadeToScene();
    }
}
