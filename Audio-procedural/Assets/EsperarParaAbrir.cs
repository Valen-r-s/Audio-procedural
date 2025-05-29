using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsperarParaAbrir : MonoBehaviour
{
    public GameObject ActiveObject;
    public float delay;
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("AbrirPuerta", delay);
    }

    private void AbrirPuerta()
    {
        ActiveObject.SetActive(true);
    }
}
