using System.Collections;
using UnityEngine;

public class LancementSimulation : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private Animator monAnimator;
    [Tooltip("Temps en secondes avant que l'animation ne se lance")]
    [SerializeField] private float delaiEnSecondes = 5.0f;

    [Tooltip("Le nom du paramètre Trigger dans votre Animator Controller")]
    [SerializeField] private string nomDuTrigger = "Lancer";

    void Start()
    {
        // On démarre le chrono dès que l'objet apparaît
        StartCoroutine(ChronoLancement());
    }

    // Cette fonction permet de mettre le code en pause pendant X secondes
    IEnumerator ChronoLancement()
    {
        // 1. On attend
        yield return new WaitForSeconds(delaiEnSecondes);

        // 2. On active l'animation
        if (monAnimator != null)
        {
            monAnimator.SetTrigger(nomDuTrigger);
            Debug.Log("Délai écoulé : Animation lancée !");
        }
        else
        {
            Debug.LogWarning("Attention : Aucun Animator assigné dans le script LancementSimulation !");
        }
    }
}