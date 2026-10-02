using Nan.Client;
using Nan.Connection;

class Program
{
    public static async Task Main( string[] args )
    {
        FakeConnection fk = new FakeConnection();
        Client c = new Client( fk );
        
        if( args.Length == 0 ) 
        {
            c.ReceiveMessage();
        }
        else 
        {
            c.SendMessage("123");
        }
    }
}
