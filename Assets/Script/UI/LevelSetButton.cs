using UnityEngine;

public class LevelSetButton : MonoBehaviour
{

    [SerializeField] public int value ;
    [SerializeField] public WorldManager worldManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue()
    {
        worldManager.setLevel(value);
    }
}
