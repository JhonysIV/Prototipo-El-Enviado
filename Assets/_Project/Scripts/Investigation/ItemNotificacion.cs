using UnityEngine;
using TMPro;
using System.Collections;

public class ItemNotificacion : MonoBehaviour
{
    public TextMeshProUGUI textoTMP;

    public void MostrarMensaje(string nombreObjeto)
    {
        StopAllCoroutines();
        textoTMP.text = "Anotado: " + nombreObjeto;
        gameObject.SetActive(true);
        StartCoroutine(OcultarTrasTiempo());
    }

    private IEnumerator OcultarTrasTiempo()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}