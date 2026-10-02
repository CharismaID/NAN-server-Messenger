using System.Text;
using System.Net.Sockets;

using Nan.Connection;

namespace Nan.Client
{

public sealed class Client : IDisposable
{
    private readonly IConnection _connection;

    public Client(IConnection connection)
    {
        _connection = connection;
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        
        _connection.Send(data);
    }

    public string? ReceiveMessage()
    {
        byte[] buffer = new byte[4096];

        int received = _connection.Receive(buffer);

        if (received == 0)
        {
            return null;
        }

        return Encoding.UTF8.GetString(buffer, 0, received);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}

}
