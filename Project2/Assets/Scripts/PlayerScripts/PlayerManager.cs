using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager: Singleton<PlayerManager>
{
    public GameObject PlayerPrefab;

    public static GameObject player;
    [SerializeField] Vector2 PlayerPositionIndexes;

    public delegate void PlayerSpawnedEventHandler(GameObject playerObject);

    public static event PlayerSpawnedEventHandler OnPlayerSpawned;

    public static event PlayerSpawnedEventHandler OnPlayerDestroyed;

    public float scale = 12f;


    private void SpawnPlayer(GameState state){
        if (state != GameState.Playing) return;
        Debug.Log("Spawning the player");

        player = Instantiate(PlayerPrefab, new Vector3(PlayerPositionIndexes.x * scale + 6, 0.5f, PlayerPositionIndexes.y * scale+ 6), Quaternion.identity);
        player.GetComponent<PlayerController>().alive = true;
        Debug.Log(player);
        OnPlayerSpawned?.Invoke(player);
        //stop
        
        
    }

    public void RestartPlayer(GameState state){
        if (state != GameState.Restart) return;
        Debug.Log("Restarting the player");
        player.GetComponent<PlayerController>().alive = false;
        Destroy(player);

        
        GameManager.Instance.UpdateGameState(GameState.Playing);

        //OnPlayerDestroyed?.Invoke(null);
        
        
    }



    protected override void Awake()
    {
        GameManager.OnGameStateChanged += SpawnPlayer;
        GameManager.OnGameStateChanged += RestartPlayer;
    }
    protected override void OnDestroy()
    {
        GameManager.OnGameStateChanged -= SpawnPlayer;
        GameManager.OnGameStateChanged -= RestartPlayer;
        base.OnDestroy();
    }
}