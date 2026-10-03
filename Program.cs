using Nan.Peer;
using System.Net;

class Program
{
    public static void Main( string[] args )
    {
		IPAddress addr;
		short port;
		if( args.Length == 2 ) 
		{
			try
			{
				addr = IPAddress.Parse(args[0]);
				port = short.Parse(args[1]);
			}
			catch( Exception )
			{
				Console.WriteLine("usage: dotnet run start listener on loopback and port 12345, dotnet run *ip address* *port* start writer");
				return;
			}
		}
		else 
		{
			addr = IPAddress.Parse("127.0.0.1");
			port = 12346;
		}

		var endpoint = new IPEndPoint( addr, port );
        var c = new DeviceSocket( endpoint );
        
		// start reader
        if( args.Length == 0 ) 
        {
			var contact = c.WaitForContact();
            Console.WriteLine( contact.ReceiveMessage() );
        }
		// else start writer
        else 
        {
			endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12346);
			var contact = new Contact(endpoint);
            contact.SendMessage("123");
        }
    }
}
