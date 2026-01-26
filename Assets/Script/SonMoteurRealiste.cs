using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SonMoteurNet : MonoBehaviour
{
    [Header("Configuration")]
    public float vitessePourPitchMax = 15.0f;
    public float pitchAuRalenti = 0.8f;
    public float pitchMax = 2.0f;

    [Header("Réglages Arrêt")]
    public float vitesseDeTransition = 5.0f;
    public float tempsAvantCoupure = 3.0f; // Temps immobile avant l'arrêt
    public float seuilImmobilite = 0.1f;

    private AudioSource audioSource;
    private Vector3 positionPrecedente;
    private float chronoImmobilite;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        positionPrecedente = transform.position;
        if (!audioSource.isPlaying) audioSource.Play();
    }

    void Update()
    {
        // 1. CALCUL DE LA VITESSE
        float distanceParcourue = (transform.position - positionPrecedente).magnitude;
        float vitesseReelle = distanceParcourue / Time.deltaTime;
        positionPrecedente = transform.position;

        // 2. LOGIQUE DE COUPURE NETTE
        if (vitesseReelle < seuilImmobilite)
        {
            chronoImmobilite += Time.deltaTime;

            // Si le temps est écoulé et que le son joue encore : ON COUPE
            if (chronoImmobilite >= tempsAvantCoupure && audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("Contact coupé : Moteur arrêté.");
            }
        }
        else
        {
            chronoImmobilite = 0f;
            // Si le camion bouge et que le moteur était éteint : ON RELANCE
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        // 3. GESTION DU PITCH (Seulement si le moteur tourne)
        if (audioSource.isPlaying)
        {
            float pourcentageVitesse = Mathf.Clamp01(vitesseReelle / vitessePourPitchMax);
            float pitchCible = Mathf.Lerp(pitchAuRalenti, pitchMax, pourcentageVitesse);
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitchCible, Time.deltaTime * vitesseDeTransition);
        }
    }
}