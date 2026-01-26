using System.Collections;
using UnityEngine;
using UnityEngine;

public class DetecteurCamion : MonoBehaviour
{
    [Header("Liaisons")]
    public Animator animatorCible;      

    [Header("Réglages")]
    public string tagRequis = "Camion"; 
    public string triggerAnimation = "Ouvrir";

    private bool estActive = false;
    private bool dejaActive = false;
    [Tooltip("Temps d'attente en secondes avant de lancer l'animation")]
    public float delaiAvantAnim = 7.0f;
    private bool compteAReboursLance = false;
    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est bien un camion qui entre
        if (other.CompareTag(tagRequis))
        {
            estActive = true;
            Debug.Log("Zone activée par : " + other.name);
            VerifierEtLancer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si le camion sort, on désactive
        if (other.CompareTag(tagRequis))
        {
            estActive = false;
        }
    }

    void VerifierEtLancer()
    {
        // Condition : JE suis activé, L'AUTRE est activé, et on n'a pas déjà lancé le chrono
        if (estActive && !dejaActive  && !compteAReboursLance)
        {
            // Au lieu de lancer l'anim tout de suite, on démarre le chrono
            StartCoroutine(LancerAvecDelai());
        }
    }

    // C'est cette fonction spéciale qui gère l'attente
    IEnumerator LancerAvecDelai()
    {
        // 1. On verrouille pour ne pas relancer le chrono en boucle
        compteAReboursLance = true;
        Debug.Log("Conditions remplies. Attente de " + delaiAvantAnim + " secondes...");

        // 2. PAUSE : Le code s'arrête ici pendant X secondes
        yield return new WaitForSeconds(delaiAvantAnim);

        // 3. Après la pause, on vérifie si l'animator existe et on lance
        if (animatorCible != null)
        {
            animatorCible.SetTrigger(triggerAnimation);
            Debug.Log("Délai terminé : Animation lancée !");
        }

        // Note : On ne remet pas compteAReboursLance à false ici, 
        // pour que l'animation ne se joue qu'une seule fois.
    }
}