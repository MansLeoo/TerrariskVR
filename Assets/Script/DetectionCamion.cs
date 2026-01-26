using System.Collections;
using UnityEngine;

public class DetecteurCamionSequenceComplete : MonoBehaviour
{
    [Header("Liaisons")]
    public Animator animatorCible;
    public GameObject objetADisparaitre; // L'objet qui s'efface au début
    public GameObject objetAApparaitre;  // Le NOUVEL objet qui apparaît pendant l'anim

    [Header("Audio")]
    public AudioSource audioPreparation;
    public AudioSource audioAnimation;

    [Header("Réglages Temporels")]
    public float delaiAvantDisparition = 2.0f;
    public float delaiApresDisparition = 3.0f;

    [Tooltip("Délai après le DEBUT de l'animation avant de montrer l'objet")]
    public float delaiApparitionPendantAnim = 1.0f;
    [Tooltip("Durée de visibilité de cet objet")]
    public float dureeVisibiliteObjet = 5.0f;

    [Header("Paramètres")]
    public string tagRequis = "Camion";
    public string triggerAnimation = "Ouvrir";

    private bool estActive = false;
    private bool compteAReboursLance = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagRequis))
        {
            estActive = true;
            VerifierEtLancer();
        }
    }

    void VerifierEtLancer()
    {
        if (estActive && !compteAReboursLance)
        {
            StartCoroutine(SequenceComplete());
        }
    }

    IEnumerator SequenceComplete()
    {
        compteAReboursLance = true;

        // --- PHASE 1 : ATTENTE INITIALE ---
        yield return new WaitForSeconds(delaiAvantDisparition);

        // --- PHASE 2 : DISPARITION ---
        if (objetADisparaitre != null) objetADisparaitre.SetActive(false);
        if (audioPreparation != null) audioPreparation.Play();

        // --- PHASE 3 : DÉLAI INTERMÉDIAIRE ---
        yield return new WaitForSeconds(delaiApresDisparition);

        // --- PHASE 4 : ANIMATION & APPARITION ---
        if (animatorCible != null)
        {
            animatorCible.SetTrigger(triggerAnimation);
            if (audioAnimation != null) audioAnimation.Play();

            // Attendre 1 seconde avant de faire apparaître le nouvel objet
            yield return new WaitForSeconds(delaiApparitionPendantAnim);
            if (objetAApparaitre != null) objetAApparaitre.SetActive(true);

            // Attendre 5 secondes avant de le faire disparaître
            yield return new WaitForSeconds(dureeVisibiliteObjet);
            if (objetAApparaitre != null) objetAApparaitre.SetActive(false);

            // Optionnel : On attend la fin totale des 7s de son (si besoin de synchroniser)
            // yield return new WaitForSeconds(1); 

            if (audioAnimation != null) audioAnimation.Stop();
        }
    }
}