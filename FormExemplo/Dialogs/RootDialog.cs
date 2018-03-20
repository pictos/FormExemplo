using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace FormExemplo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Olá sou o bot de aluguel de carros");
            await Resposta(context);

            context.Wait(MessageReceivedAsync);
        }

        static async Task Resposta(IDialogContext contexto)
        {
            await contexto.PostAsync("Você gostaria de alugar um carro?");
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            //context.Wait(MessageReceivedAsync);
            await Resposta(context);
            context.Done(activity);
        }
    }
}