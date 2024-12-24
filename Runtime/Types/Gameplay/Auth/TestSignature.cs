namespace CiFarm.Types.Gameplay.Auth
{
    public class TestSignatureRequest
    {
        public string ChainKey { get; set; }
        public int AccountNumber { get; set; }
        public string Network { get; set; }
    }

    public class TestSignatureResponse
    {
        public string ChainKey { get; set; }
        public string Message { get; set; }
        public string PublicKey { get; set; }
        public string Signature { get; set; }
        public string Network { get; set; }
        public string BotType { get; set; }
        public string AccountAddress { get; set; }
    }
}