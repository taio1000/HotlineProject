using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    private Player player;
    


    [SerializeField] public GameObject midGoal, finishGoal;
    [SerializeField] private Vector3 StartPosition;
    [SerializeField] public int cardsInLevel, levelsCompleted;
    [SerializeField] private GameDataController datacontroller;
    [SerializeField] private LoseMenu loseMenu;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Hay mas de un gameManager");
        }
        player = FindObjectOfType<Player>();
        datacontroller = FindObjectOfType<GameDataController>();
        loseMenu = FindObjectOfType<LoseMenu>();
    }

    void Start() {
        if(finishGoal != null)
            finishGoal.SetActive(false);
        if(midGoal != null)
            midGoal.SetActive(false);

    }
   

    // Update is called once per frame
    void Update()
    {
        if(cardsInLevel == 0)
        {
            if(midGoal != null)
            {
                midGoal.SetActive(true);
            }
            else
            {
                if(finishGoal != null)
                {
                    finishGoal.SetActive(true);
                }
                
            }
        }
        
    }

    public void DecreaseCard()
    {
        cardsInLevel--; 
    }
    public void EndLevel()
    {
        loseMenu.PauseGame();
    }
}
