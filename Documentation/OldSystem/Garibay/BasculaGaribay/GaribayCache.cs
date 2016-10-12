using Microsoft.Synchronization.Data;
namespace BasculaGaribay {
    
    
    public partial class GaribayCacheSyncAgent {
        
        partial void OnInitialized(){
            this.dbo_Boletas.SyncDirection = SyncDirection.Bidirectional;
        }
    }
}
