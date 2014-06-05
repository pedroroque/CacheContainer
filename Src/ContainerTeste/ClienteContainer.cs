using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheContainer;

namespace ContainerTeste
{
    public class ClienteContainer : ContainerIndexado<int, ClienteVO>
    {
        private ClienteContainer()
        {
        }

        protected override long TempoDeValidadeEmSegundos
        {
            get
            {
                return 2; //segundos
            }
        }

        protected override long TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos
        {
            get
            {
                return 1; //segundo
            }
        }
        
        public int ErroNaColeta { get; set; }

        public int NumeroDaColeta
        {
            get { return CargasRealizadas + 1; }
        }

        protected override List<ClienteVO> ColetarDados()
        {
            //Simulação de ida ao serviço e coleta de dados
            try
            {
                if (ErroNaColeta == NumeroDaColeta)
                {
                    ErroNaColeta = 0; //Passar a não gerar exceção na coleta de dados.
                    throw new Exception(string.Format("Erro na Coleta de Dados número {0}", CargasRealizadas));
                }

                var novosDados = new List<ClienteVO>
                {
                    new ClienteVO(1, "Cliente 1", DateTime.Now),
                    new ClienteVO(2, "Cliente 2", DateTime.Now),
                    new ClienteVO(3, "Cliente 3", DateTime.Now),
                    new ClienteVO(4, "Cliente 4", DateTime.Now),
                    new ClienteVO(5, "Cliente 5", DateTime.Now),
                    new ClienteVO(6, "Cliente 6", DateTime.Now),
                    new ClienteVO(7, "Cliente 7", DateTime.Now),
                    new ClienteVO(8, "Cliente 8", DateTime.Now),
                    new ClienteVO(9, "Cliente 9", DateTime.Now)
                };

                return novosDados;
            }
            catch (Exception ex)
            {
                //Logar ex;

                //Informar ao Container que ocorreu erro ao executar a Coleta de Dados
                return null;
            }
        }

        public static ClienteContainer GetInstance()
        {
            if (instanciaUnica == null)
                instanciaUnica = new ClienteContainer();

            return (ClienteContainer)instanciaUnica;
        }

        public static ClienteContainer NovoParaTeste()
        {
            instanciaUnica = new ClienteContainer();

            return (ClienteContainer)instanciaUnica;
        }

    }
}
