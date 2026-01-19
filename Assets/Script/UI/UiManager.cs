using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance { get; private set; }


    [SerializeField] private GameObject InformationPage;
    [SerializeField] private GameObject PrincipalePage;
    private void Awake()
    {
        // Logique Singleton : S'assurer qu'il n'y a qu'un seul UIManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherInformation()
    {
        PrincipalePage.SetActive(false);
        InformationPage.SetActive(true);

    }
    public void AfficherPrincipale()
    {
        PrincipalePage.SetActive(true);
        InformationPage.SetActive(false);
    }
}
