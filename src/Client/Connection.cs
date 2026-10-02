using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Nan.Connection
{

public interface IConnection : IDisposable
{
    EndPoint? RemoteEndPoint { get; }

    int Receive(byte[] buffer);

    void Send(byte[] data);
}

public sealed class FakeConnection : IConnection
{
    public string? LastSentMessage { get; private set; }

    public EndPoint? RemoteEndPoint => null;

    public int Receive(byte[] buffer)
    {
        byte[] response = Encoding.UTF8.GetBytes("Fake response");

        Array.Copy(response, buffer, response.Length);

        return response.Length;
    }

    public void Send(byte[] data)
    {
        LastSentMessage = Encoding.UTF8.GetString(data);
    }

    public void Dispose()
    {
    }
}

}
