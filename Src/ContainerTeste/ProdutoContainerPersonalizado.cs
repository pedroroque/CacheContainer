using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using CacheContainer;

namespace ContainerTeste
{
    /// <summary>
    /// O Container ProdutoContainerPersonalizado pode ser para o tipo ProdutoVO, mesmo este Tipo de Dado já ser armazenado por ProdutoContainer
    /// Tipos diferentes de container podem armazenar o mesmo tipo de dado sem que um interfira no outro.
    /// </summary>
    public class ProdutoContainerPersonalizado : Container<ProdutoVO>
    {
        private ProdutoContainerPersonalizado()
        {
        }

        protected override long TempoDeValidadeEmSegundos
        {
            get
            {
                return 2; //segundos
            }
        }

        protected override List<ProdutoVO> ColetarDados()
        {
            try
            {
                var novosDados = new List<ProdutoVO>
                {
                    new ProdutoVO(1, "Produto 1", DateTime.Now),
                    new ProdutoVO(2, "Produto 2", DateTime.Now),
                    new ProdutoVO(3, "Produto 3", DateTime.Now)
                };

                /*
                novosDados.Add(new ProdutoVO(4, "Produto 4", DateTime.Now));
                novosDados.Add(new ProdutoVO(5, "Produto 5", DateTime.Now));
                novosDados.Add(new ProdutoVO(6, "Produto 6", DateTime.Now));
                novosDados.Add(new ProdutoVO(7, "Produto 7", DateTime.Now));
                novosDados.Add(new ProdutoVO(8, "Produto 8", DateTime.Now));
                novosDados.Add(new ProdutoVO(9, "Produto 9", DateTime.Now));
                */

                return novosDados;
            }
            catch (Exception ex)
            {
                //Logar ex;

                //Informar ao Container que ocorreu erro ao executar a Coleta de Dados
                return null;
            }
        }

        private Dictionary<int, ProdutoVO> IndexadoPorId;
        private Dictionary<string, ProdutoVO> IndexadoPorNome;

        public ProdutoVO ObterPorId(int id)
        {
            VerificarValidade();

            if (IndexadoPorId.ContainsKey(id))
                return IndexadoPorId[id];

            return null;
        }

        public ProdutoVO ObterPorNome(string nome)
        {
            VerificarValidade();

            if (IndexadoPorNome.ContainsKey(nome))
                return IndexadoPorNome[nome];

            return null;
        }

        protected override void ExecutarRenovacaoDosDadosPontoDeExtensao(List<ProdutoVO> novosDados)
        {
            IndexadoPorId = new Dictionary<int, ProdutoVO>();
            IndexadoPorNome = new Dictionary<string, ProdutoVO>();

            foreach (var valor in novosDados)
            {
                IndexadoPorId.Add(valor.Id, valor);
                IndexadoPorNome.Add(valor.Nome, valor);
            }
        }

        public static ProdutoContainerPersonalizado GetInstance()
        {
            if (instanciaUnica == null)
                instanciaUnica = new ProdutoContainerPersonalizado();

            return (ProdutoContainerPersonalizado) instanciaUnica;
        }
    }
}
