using System.Text;
using System.Net;
using System.Net.Sockets;

// P2P usecase:

namespace Nan.Client
{

public class Client : IDisposable
{
	private Socket _socket;
	private Socket? _contact;

    public Client( IPEndPoint? endpoint )
    {
		endpoint ??= new IPEndPoint(IPAddress.Parse("0.0.0.0"), 12345);

		_socket = new Socket(
			AddressFamily.InterNetwork,
			SocketType.Stream,
			ProtocolType.Unspecified
		);

		_socket.Bind( endpoint );
    }

	public void Connect(IPEndPoint endpoint )
	{
		_socket.Connect( endpoint );
	}

    public void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        
        _socket.Send( data );
    }

    public string ReceiveMessage()
    {
		_socket.Listen();
		_contact = _socket.Accept();
        byte[] buffer = new byte[4096];

        int received = _contact.Receive( buffer );

        return Encoding.UTF8.GetString(buffer, 0, received);
    }

    public void Dispose()
    {
		_socket.Close();
    }
}

}
