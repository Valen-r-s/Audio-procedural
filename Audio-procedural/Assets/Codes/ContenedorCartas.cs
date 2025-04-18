using UnityEngine;
using System.Collections.Generic;

public class ContenedorCartas : MonoBehaviour
{
    public List<GameObject> cartas;
    public RaycastInteractor raycastRef; // Arrastrar en el inspector
    public Animator Animator;

    public void ActualizarCartaVisual()
    {
        int index = raycastRef.GetEquippedCardIndex();
        
        for (int i = 0; i < cartas.Count; i++)
        {
            cartas[i].SetActive(i == index);
        }

        Animator.SetInteger("EstadoCarta", 0); // Volver a estado normal después de cambio
        raycastRef.SetIsChangingCard(false);
        Debug.Log("Carta visual actualizada a: " + (index + 1));
    }

    public void EmpezarApuntar()
    {
        raycastRef.SetIsAiming(true);
        Debug.Log("Inicio de apuntado (desde animación)");
    }

    public void TerminarApuntar()
    {
        raycastRef.SetIsAiming(false);
        Debug.Log("Fin de apuntado (desde animación)");
    }


}

