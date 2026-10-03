using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Nan.Peer
{

/*
   What this class should do in future
	1. Async accept connections out of class, where they saves into Contact list
	2. Aggregate Availablity notifier? 
*/

// passive socket class, that accept connections
public class DeviceSocket
{
	private Socket _socket;

    public DeviceSocket( IPEndPoint? endpoint )
    {
		endpoint ??= new IPEndPoint(IPAddress.Parse("0.0.0.0"), 12345);

		_socket = new Socket(
			AddressFamily.InterNetwork,
			SocketType.Stream,
			ProtocolType.Unspecified
		);

		_socket.Bind( endpoint );
		_socket.Listen();
    }

	public Contact WaitForContact()
	{
		return new Contact(_socket.Accept());
	}
}

}
