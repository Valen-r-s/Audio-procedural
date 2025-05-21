using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalLvl2Script : MonoBehaviour
{
    // Start is called before the first frame update
    public ScreenFader FadeCode;
    public GameObject Ligth;
    public void LigthsOff()
    {
        Ligth.SetActive(false);
        FadeCode.FadeToScene();
    }
}
