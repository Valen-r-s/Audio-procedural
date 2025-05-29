using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODGlobalParameterSetter : MonoBehaviour
{
    [Header("Parámetro Global")]
    public string parameterName = "Parameter 1";
    public int parameterValue = 0;

    [Header("Trigger Settings")]
    public bool useTrigger = true;
    public string playerTag = "Player";


    public void SetGlobalFMODParameter(int value)
    {
        parameterValue = value;
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
        Debug.Log($"🌍 Parámetro global '{parameterName}' seteado a {parameterValue}");
    }
}
