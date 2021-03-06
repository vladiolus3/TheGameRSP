using RSPGame.Models.GameModel;

namespace RSPGame.Models
{
    public class Session
    {
        public string UserName { get; set; }
        
        public string Token { get; set; }
        
        public int CountLoginFailed { get; set; }
        
        public GamerInfo GamerInfo { get; set; }
    }
}