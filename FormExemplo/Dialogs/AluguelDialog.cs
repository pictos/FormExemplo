using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace FormExemplo.Dialogs
{
    public class AluguelDialog
    {
        public static readonly IDialog<string> dialogo = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
            new RegexCase<IDialog<string>>(new Regex("olá", RegexOptions.IgnoreCase), (contexto, texto) =>
            {
                return Chain.ContinueWith(new RootDialog(), FimForm);
            }),
            new DefaultCase<string, IDialog<string>>((contexto, texto) =>
            {
                
                return Chain.ContinueWith(FormDialog.FromForm(Modelos.Aluguel.BuildForm, FormOptions.PromptInStart), FimForm);
            }))
            .Unwrap()
            .PostToUser();

        private async static Task<IDialog<string>> FimForm(IBotContext contexto, IAwaitable<object> item)
        {
            var token = await item;
            
            return Chain.Return("Obrigado por utilizar nosso serviço");

        }
    }
}