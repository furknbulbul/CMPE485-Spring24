using System.Collections.Generic;
using System.Diagnostics;

public class TrapManager : Singleton<TrapManager>
{
    private List<SpikeTrap> spikeTraps = new List<SpikeTrap>();

    protected override void Awake(){
        GameManager.OnGameStateChanged += RestartAllSpearTraps;
        
    }
    protected override void OnDestroy(){
        GameManager.OnGameStateChanged -= RestartAllSpearTraps;
        base.OnDestroy();
    }   
    
    void Start()
    {
        
    }

    

    public void RestartAllSpearTraps(GameState state){
        if (state != GameState.Restart) return;
        spikeTraps.Clear();
        spikeTraps.AddRange(FindObjectsOfType<SpikeTrap>());
    
        for (int i = 0; i < spikeTraps.Count; i++){
            spikeTraps[i].RestartTrap();
        }

        print("Restarting all traps");
        
       
    }
}
