

using UnityEngine;
using System.Collections;

public class DelaiEboulement : MonoBehaviour
{
    [Header("Réglages")]
    public Animator monAnimator; // Glissez votre objet ici
    public string nomDuTrigger = "LanceToi"; // Le nom du paramètre dans l'Animator
    public float tempsAttente = 10.0f; // Durée en secondes
    public GameObject objectToDisable; // Glissez votre objet ici

    void Start()
    {
        // On lance le compte à rebours dès le début du jeu
        StartCoroutine(LancerAnimation());
    }

    IEnumerator LancerAnimation()
    {
        Debug.Log("Compte à rebours commencé : " + tempsAttente + " secondes.");

        // On attend la durée définie
        yield return new WaitForSeconds(tempsAttente);

        // On active l'animation
        if (monAnimator != null)
        {
            objectToDisable.SetActive(false);
            monAnimator.SetTrigger(nomDuTrigger);
            Debug.Log("Éboulement déclenché !");
        }
        else
        {
            Debug.LogError("Attention : L'Animator n'est pas assigné dans l'inspecteur !");
        }
    }
}