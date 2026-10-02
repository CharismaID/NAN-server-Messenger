using System.Text;
using System.Net.Sockets;

class Program
{
    static public async Task Main( string[] args )
    {
        if( File.Exists(socketPath))
            File.Delete(socketPath);

        Socket s;

        try 
        {
            s = new Socket(
                    AddressFamily.Unix,
                    SocketType.Stream,
                    ProtocolType.Unspecified
            );

            var endpoint = new UnixDomainSocketEndPoint(socketPath);
            s.Bind(endpoint) ;
            s.Listen();
        }
        catch( Exception e )
        {
            Console.WriteLine($"Exception handles: {e.Data}");
            return;
        }

        Console.WriteLine($"Listening on {socketPath}");

        while (true)
        {
            using Socket client = await s.AcceptAsync();

            byte[] buffer = new byte[1024];
            int bytesRead = await client.ReceiveAsync(buffer);

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received: {message}");

            byte[] response = Encoding.UTF8.GetBytes($"Server received: {message}");
            await client.SendAsync(response);
        }
    }

    const string socketPath = "/tmp/myapp.sock";

}
