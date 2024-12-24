namespace CiFarm.Types.Gameplay.Auth
{
    public class VerifySignatureRequest
    {
        public string Message { get; set; }
        public string PublicKey { get; set; }
        public string Signature { get; set; }
        public string ChainKey { get; set; }
        public string Network { get; set; }
        public string AccountAddress { get; set; }
    }

    public class VerifySignatureResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}