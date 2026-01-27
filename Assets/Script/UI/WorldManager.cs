using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{

    public static WorldManager Instance { get; private set; }


    private int currentLevel;
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
    public void setLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        loadLevel();
    }
    public void addLevel(int levelIndex)
    {
        currentLevel++;
        loadLevel();
    }
    public void minusLevel(int levelIndex)
    {
        currentLevel--;
        loadLevel();
    }
    public void loadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }
    public void gotoPrincipale()
    {
        SceneManager.LoadScene(0);
    }
    public void resetScene()
    {
        Scene sceneActive = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneActive.name);
    }
}
