using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public static PauseManager Instance { get; private set; }

    private bool onPause;
    [SerializeField] private List<GameObject> objectsToControl = new List<GameObject>();
    private List<bool> previousObjectState = new List<bool>();
    [SerializeField] private GameObject pauseMenu;
    private WorldManager worldManager;

    private void Awake()
    {
        // Logique Singleton : S'assurer qu'il n'y a qu'un seul UIManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        onPause = false;
        DontDestroyOnLoad(gameObject);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldManager = WorldManager.Instance;
    }

    // Update is called once per frame
    public void SetStatus(bool status)
    {
       onPause = status;
    }
    public void SetPause()
    {
        onPause =  true;
        Time.timeScale = 0;
        DeactivateAll();
        pauseMenu.SetActive(true);

    }
    public void StopPause()
    {
       onPause = false ;
       Time.timeScale = 1 ;
       ActivateAll();
       pauseMenu.SetActive(false);

    }
    public bool IsPause()
    {
        return onPause;
    }
    public void ActivateAll()
    {
        for (int i = 0; i < objectsToControl.Count; i++)
        {
            GameObject obj = objectsToControl[i];

            if (obj != null && i < previousObjectState.Count)
            {
                obj.SetActive(previousObjectState[i]);
            }
        }
    }

    public void DeactivateAll()
    {
        previousObjectState.Clear();

        for (int i = 0; i < objectsToControl.Count; i++)
        {
            GameObject obj = objectsToControl[i];

            if (obj != null)
            {
                previousObjectState.Add(obj.activeSelf);
                obj.SetActive(false);
            }
            else
            {
                previousObjectState.Add(false);
            }
        }
    }
    public void ReinitialiserScene()
    {
        worldManager.resetScene();
        Reprendre();
    }
    public void MenuPrincipale()
    {
        worldManager.gotoPrincipale();
        Reprendre();
    }
    public void Reprendre()
    {
        this.StopPause();
    }
}
