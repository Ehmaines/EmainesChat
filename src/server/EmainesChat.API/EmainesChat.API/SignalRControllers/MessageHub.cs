using Microsoft.AspNetCore.SignalR;

namespace EmainesChat.API.SignalRControllers
{
    public class MessageHub : Hub
    {
        public async Task SendMensagem(string mensagem)
        {
            await Clients.All.SendAsync("ReceberMensagem", mensagem); // Envia uma mensagem para todos os clientes conectados
        }
    }
}
