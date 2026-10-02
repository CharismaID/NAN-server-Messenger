using Nan.Client;
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
        Client c = new Client( endpoint );
        
        if( args.Length == 0 ) 
        {
            Console.WriteLine(c.ReceiveMessage());
        }
        else 
        {
			var contact = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12346);
			c.Connect( contact );
            c.SendMessage("123");
        }
    }
}
