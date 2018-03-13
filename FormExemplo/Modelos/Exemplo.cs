using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace FormExemplo.Modelos
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe não entendi \"{0}\".")]
    public class Exemplo
    {
        [Prompt("Qual {&} você deseja?{||}", ChoiceFormat = "{1}")]
        public Salgadinhos Salgadinho { get; set; }
        [Prompt("O que deseja beber? {||}")]
        public Bebidas Bebidas { get; set; }
        //[Prompt("Selecione o tipo de entrega, motoboy será adiciona R$2,00 ao valor final")]
        public TipoEntrega TipoEntrega { get; set; }
        public CPFNaNota CPFNaNota { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }


        public static IForm<Exemplo> BuildForm()
        {

            return new FormBuilder<Exemplo>()
                 .Message("Bem vindo!")
                 .Field(nameof(Salgadinho))
                 .Field(nameof(Bebidas))
                 .Field(nameof(TipoEntrega))
                 .AddRemainingFields() //Adicona os outros campos
                 .Confirm("{Nome}.<br/> Você escolheu um {Salgadinho} <br/> acompanhado de uma deliciosa {Bebidas}") //Para mudar a mensagem de confirmação
                 .OnCompletion(async (c, p) =>
                 {
                     await c.PostAsync("Pedido sendo processado");
                 })
                 .Build();

        }


    }
    #region Pedidos
    [Describe("Tipo de Entrega")]
    public enum TipoEntrega
    {
        [Terms("Retirar No Local", "Passo ai", "Eu pego", "Retiro ai")]
        [Describe("Retirar No Local")]
        RetirarNoLocal = 1,

        [Terms("Motoboy", "Motoca", "Cachorro Loco", "Entrega", "Em casa")]
        [Describe("Motoboy")]
        Motoboy
    }

    [Describe("Salgados")]
    public enum Salgadinhos
    {
        [Terms("Esfirra", "Isfirra", "Esfira", "Isfira", "i")]
        [Describe("Esfirra")]
        Esfirra = 1,

        [Terms("Quibe", "Kibe", "k", "q")]
        [Describe("Quibe")]
        Quibe,

        [Terms("Coxinha", "Cochinha", "Coxa", "c")]
        [Describe("Coxinha")]
        Coxinha
    }

    [Describe("Bebidas")]
    public enum Bebidas
    {
        [Terms("Água", "agua", "h2o", "a")]
        [Describe("Água")]
        Agua = 1,

        [Terms("Refrigerante", "refri", "r")]
        [Describe("Refrigerante")]
        Refrigerante,

        [Terms("Suco", "s")]
        [Describe("Suco")]
        Suco
    }

    [Describe("CPF na Nota")]
    public enum CPFNaNota
    {
        [Terms("Sim", "s", "yep")]
        [Describe("Sim")]
        Sim = 1,

        [Terms("Não", "n", "nao")]
        [Describe("Não")]
        Nao
    }
    #endregion


}