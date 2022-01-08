using System.Net;

namespace Infra.Infra
{
    public static class Settings
    {
        public static string DockerHostMachineIpAddress => Dns.GetHostAddresses(new Uri("http://docker.for.win.localhost").Host)[0].ToString();

        public static string ConnectionString = $@"Server=localhost,1433;Database=Colegio;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True;";

    }
    
}
