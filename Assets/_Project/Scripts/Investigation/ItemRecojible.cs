using UnityEngine;

public class ItemRecojible : MonoBehaviour
{
    [Header("Interfaz de Usuario")]
    public GameObject promptUI;             // Objeto Intereaccion Text ([E] Recoger)
    public ItemNotificacion notificacionUI; // Objeto TextoNotificacion
    public JournalMenu diarioMenu;          // Referencia al menú del diario

    [Header("Datos del Objeto")]
    public string nombreObjeto = "Esfera Luminosa";
    
    [TextArea(2, 5)]
    public string descripcionObjeto = "Es extraño como en este mundo pueden existir estos objetos";

    private bool isPlayerNearby = false;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

        // Si no asignaste el diario en el Inspector, lo busca automáticamente en la escena
        if (diarioMenu == null)
        {
            diarioMenu = FindFirstObjectByType<JournalMenu>();
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            RecogerObjeto();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool esJugador = other.CompareTag("Player") || 
                         (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (esJugador)
        {
            isPlayerNearby = true;
            if (promptUI != null)
            {
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool esJugador = other.CompareTag("Player") || 
                         (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (esJugador)
        {
            isPlayerNearby = false;
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }

    private void RecogerObjeto()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }

        if (notificacionUI != null)
        {
            notificacionUI.MostrarMensaje(nombreObjeto);
        }

        // Anota la entrada en el diario antes de destruir el objeto
        if (diarioMenu != null)
        {
            diarioMenu.RegistrarEntrada(nombreObjeto, descripcionObjeto);
        }

        Debug.Log("¡Objeto recolectado y guardado en diario: " + nombreObjeto + "!");

        Destroy(gameObject);
    }
}