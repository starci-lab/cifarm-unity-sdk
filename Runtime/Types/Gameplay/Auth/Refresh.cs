namespace CiFarm.Types.Gameplay.Auth
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
