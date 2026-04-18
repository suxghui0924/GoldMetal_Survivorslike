using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement plr;
    
    void Awake()
    {
        instance = this;
    }
}
