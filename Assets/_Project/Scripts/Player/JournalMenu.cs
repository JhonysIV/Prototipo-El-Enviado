using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class JournalMenu : MonoBehaviour
{
    [Header("UI Diario")]
    public GameObject panelDiario;
    public TextMeshProUGUI textoFecha;
    public TextMeshProUGUI textoPaginaIzquierda;
    public TextMeshProUGUI textoPaginaDerecha;

    [Header("Configuración de Texto")]
    public string fechaDiario = "14 de Noviembre, 1976";
    public int maxEntradasPaginaIzquierda = 3; // Cantidad de notas antes de pasar a la hoja derecha

    private List<string> entradas = new List<string>();
    private bool menuAbierto = false;

    void Start()
    {
        if (panelDiario != null)
            panelDiario.SetActive(false);

        if (textoFecha != null)
            textoFecha.text = $"<b>{fechaDiario}</b>";

        ActualizarPaginas();
    }

    void Update()
    {
        // Abre y cierra con la tecla J o TAB
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDiario();
        }
    }

    public void ToggleDiario()
    {
        menuAbierto = !menuAbierto;
        panelDiario.SetActive(menuAbierto);

        if (menuAbierto)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void RegistrarEntrada(string titulo, string descripcion)
    {
        // Formato con código HTML para color sepia/tinta vieja
        string formatoEntrada = $"<b><color=#4A2B0F>• {titulo}</color></b>\n<i><color=#2B1B17>{descripcion}</color></i>\n\n";
        
        entradas.Add(formatoEntrada);
        ActualizarPaginas();
    }

    private void ActualizarPaginas()
    {
        if (textoPaginaIzquierda == null || textoPaginaDerecha == null) return;

        textoPaginaIzquierda.text = "";
        textoPaginaDerecha.text = "";

        if (entradas.Count == 0)
        {
            textoPaginaIzquierda.text = "<i><color=#6E5A44>Aún no has registrado ningún hallazgo en estas tierras...</color></i>";
            return;
        }

        for (int i = 0; i < entradas.Count; i++)
        {
            // Las primeras entradas van a la izquierda, las siguientes a la derecha
            if (i < maxEntradasPaginaIzquierda)
            {
                textoPaginaIzquierda.text += entradas[i];
            }
            else
            {
                textoPaginaDerecha.text += entradas[i];
            }
        }
    }
}