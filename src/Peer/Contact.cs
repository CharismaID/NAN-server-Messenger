using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Nan.Peer
{
	public class Contact
	{
		private Socket _socket;

		public Contact( IPEndPoint endpoint )
		{
			_socket = new Socket(
					AddressFamily.InterNetwork,
					SocketType.Stream,
					ProtocolType.Unspecified
			);

			_socket.Connect(endpoint);
		}

		public Contact( Socket socket )
		{
			if( !socket.Connected )
				throw new ArgumentException("Given socket as contact isn't connected yet!");

			if( socket.AddressFamily != AddressFamily.InterNetwork ||
					socket.SocketType != SocketType.Stream)
				throw new ArgumentException("Given socket as contact has wrong type!");
			
			_socket = socket;
		}

		public void SendMessage(string message)
		{
			byte[] data = Encoding.UTF8.GetBytes(message);
			_socket.Send( data );
		}

		public string ReceiveMessage()
		{
			byte[] buffer = new byte[2048];
			int n = _socket.Receive( buffer );
			return Encoding.UTF8.GetString(buffer);
		}

	}
}
