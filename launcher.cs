namespace FrpAgent
{
    class launcher
    {
        public async Task RunProgram()
        {
            localFRPConfig _config = new localFRPConfig();
            await _config.ConfigChecker();

            Connector _connect = new Connector();
            _connect._uri = new Uri("ws://localhost:8765");
            await _connect.ConnectWS();

        }
    }
}
