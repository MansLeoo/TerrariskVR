using System.Collections;
using UnityEngine;

public class DelaiEboulement : MonoBehaviour
{
    [Header("Réglages")]
    public Animator monAnimator;              // Animator à déclencher
    public string nomDuTrigger = "LanceToi";   // Nom du trigger Animator
    public float tempsAttente = 10.0f;         // Temps total
    public GameObject objectToDisable;         // Objet à désactiver à t/2
    public GameObject objectToEnable;          // Objet à activer à t

    void Start()
    {
        StartCoroutine(GestionDelai());
    }

    IEnumerator GestionDelai()
    {
        Debug.Log("Début du compte à rebours : " + tempsAttente + " secondes.");

        // Attente jusqu'à la moitié du temps
        yield return new WaitForSeconds(tempsAttente / 2f);

        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
            Debug.Log("Objet désactivé à t/2");
        }

        // Attente du temps restant
        yield return new WaitForSeconds(tempsAttente / 2f);



        if (monAnimator != null)
        {
            monAnimator.SetTrigger(nomDuTrigger);
            Debug.Log("Animation déclenchée !");
        }
        else
        {
            Debug.LogError("Animator non assigné !");
        }
        yield return new WaitForSeconds(1.5f);
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("Objet activé à t");
        }
    }
}
