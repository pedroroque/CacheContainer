using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheContainer;

namespace ContainerTeste
{
    public class ProdutoContainer : ContainerIndexado<int, ProdutoVO>
    {
        private ProdutoContainer()
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
                var novosDados = new List<ProdutoVO>();

                novosDados.Add(new ProdutoVO(1, "Produto 1", DateTime.Now));
                novosDados.Add(new ProdutoVO(2, "Produto 2", DateTime.Now));
                novosDados.Add(new ProdutoVO(3, "Produto 3", DateTime.Now));
                novosDados.Add(new ProdutoVO(4, "Produto 4", DateTime.Now));
                novosDados.Add(new ProdutoVO(5, "Produto 5", DateTime.Now));
                novosDados.Add(new ProdutoVO(6, "Produto 6", DateTime.Now));
                novosDados.Add(new ProdutoVO(7, "Produto 7", DateTime.Now));
                novosDados.Add(new ProdutoVO(8, "Produto 8", DateTime.Now));
                novosDados.Add(new ProdutoVO(9, "Produto 9", DateTime.Now));

                return novosDados;
            }
            catch (Exception ex)
            {
                //Logar ex;

                //Informar ao Container que ocorreu erro ao executar a Coleta de Dados
                return null;
            }
           
        }

        public static ContainerIndexado<int, ProdutoVO> GetInstance()
        {
            return instanciaUnica ?? (instanciaUnica = new ProdutoContainer());
        }
    }
}
