using UnityEngine;

public class RotationRouesVitesse : MonoBehaviour
{
    [Header("Configurations des Roues")]
    [Tooltip("Glissez ici tous les GameObjects des roues du camion")]
    public Transform[] roues;

    [Tooltip("Diamètre de la roue en unités Unity (standard = 1.0)")]
    public float diametreRoue = 1.0f;

    [Header("Axe de Rotation")]
    public Vector3 axeDeRotation = Vector3.right; // Souvent X (Right) ou Z (Forward) selon l'import

    private Vector3 positionPrecedente;

    void Start()
    {
        positionPrecedente = transform.position;
    }

    void Update()
    {
        // 1. Calcul de la distance parcourue depuis la frame précédente
        float distanceParcourue = Vector3.Distance(transform.position, positionPrecedente);

        // 2. Déterminer le sens (avant ou arrière)
        // On regarde si le mouvement va dans la direction du camion
        float direction = Vector3.Dot(transform.forward, (transform.position - positionPrecedente).normalized);
        float multiplicateurSens = (direction >= 0) ? 1f : -1f;

        // 3. Calcul de l'angle de rotation (en degrés)
        // Formule : Angle = (Distance / Circonférence) * 360
        float circonference = Mathf.PI * diametreRoue;
        float angleRotation = (distanceParcourue / circonference) * 360f * multiplicateurSens;

        // 4. Appliquer la rotation à chaque roue
        if (distanceParcourue > 0.001f) // Évite de calculer si le camion est immobile
        {
            foreach (Transform roue in roues)
            {
                if (roue != null)
                {
                    roue.Rotate(axeDeRotation, angleRotation, Space.Self);
                }
            }
        }

        // 5. Mise à jour de la position pour la prochaine image
        positionPrecedente = transform.position;
    }
}