using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Vector3 offset =  new Vector3(0, 3, -3);

    private GameObject _player;

    

    private void Awake()
    {
        PlayerManager.OnPlayerSpawned += HandlePlayerSpawned;
    
    }
    private void OnDestroy()
    {
        PlayerManager.OnPlayerSpawned -= HandlePlayerSpawned;
    }

    void Update()
    {
     
    
    }


    void LateUpdate()
    {   
        if (_player == null) {
            Debug.Log("Player is null");   
            return;
        }        
        Vector3 newOffset = offset.x * _player.transform.right + offset.y *  _player.transform.up + offset.z * _player.transform.forward;
        
        // Update the position of the camera to follow the player
        transform.position = Vector3.Lerp(transform.position, _player.transform.position + newOffset, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, _player.transform.rotation, 0.1f);

        
    }

    private void HandlePlayerSpawned(GameObject player)
    {
        if (player == null) return;
        _player = PlayerManager.player;    
    }


    
}
