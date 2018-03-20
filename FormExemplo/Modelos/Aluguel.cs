using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;

namespace FormExemplo.Modelos
{
    [Serializable]
    public class Aluguel
    {
        #region Propriedades

        [Prompt("Por favor nos informe seu {&}:")]
        public string Nome { get; set; }

        [Prompt("Entre com sua {&}:")]
        public string CNH { get; set; }

        [Prompt("Você gostaria de um carro de qual fabricante?{||}")]
        public Fabricante Fabricantes { get; set; }

        [Optional]
        [Prompt("Escolha a quantidade de portas:{||}", ChoiceFormat ="{1}")]
        public Porta? Portas { get; set; }

        [Prompt("Qual a cor de sua preferência?{||}")]
        public Cor Cores { get; set; }

        [Prompt("Você deseja alugar o carro por quantos dias?")]
        public int Dias { get; set; }

        public List<Extra> Extras { get; set; }
        #endregion

        public static IForm<Aluguel> BuildForm()
        {
            return new FormBuilder<Aluguel>()
                //.Field(nameof(Fabricantes))
                .Message("Bem vindo, vamos iniciar o procedimento para o aluguel!")
                .OnCompletion(async (contexto, estado) =>
                {
                    await contexto.PostAsync($"{estado.Nome}, seu pedido está sendo processado! E logo poderá pilotar o {estado.Fabricantes} {estado.Cores}");
                })
                .Build();
        }
    }
    
    #region Campos

    public enum Fabricante
    {
        Audi=1,
        BMW,
        Fiat,
        Mercedes
    }

    public enum Porta
    {
        Duas=1,
        Quatro
    }

    public enum Cor
    {
        Preto=1,
        Branco,
        Prata
    }

    public enum Extra
    {
        DVD=1,
        ArCondicionado,
        Som,
        GPS
    }
    #endregion
}