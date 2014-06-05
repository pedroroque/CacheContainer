using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheContainer;
using ContainerTeste;

namespace ContainerTeste
{
    /// <summary>
    /// A definição desta classe acarretará problemas porque o Tipo CategoriaVO
    /// já possui cache definido pela classe CategoriaContainer 
    /// </summary>
    public class Categoria2Container : Container<CategoriaVO>
    {
        private Categoria2Container()
        {
        }

        protected override long TempoDeValidadeEmSegundos
        {
            get
            {
                return 2; //segundos
            }
        }

        protected override List<CategoriaVO> ColetarDados()
        {
            try
            {
                //Atenção: Os Containers Categoria2Container e Categoria2Container possuem a mesma
                //         classe base (Container) e armazenam o mesmo tipo de dado (CategoriaVO).
                //         Portanto, compartilham a mesma instancia (instanciaUnica). Então, o 
                //         método 'ColetarDados' das duas classes armazenam os dados no mesmo lugar.
                //         Em outras palavras: Vai dar errado.

                var novosDados = new List<CategoriaVO>();

                //Simulação de ida ao serviço e coleta de dados
                novosDados.Add(new CategoriaVO(1, "Categoria 1", DateTime.Now));
                novosDados.Add(new CategoriaVO(2, "Categoria 2", DateTime.Now));
                novosDados.Add(new CategoriaVO(3, "Categoria 3", DateTime.Now));
                /*
                novosDados.Add(new CategoriaVO(4, "Categoria 4", DateTime.Now));
                novosDados.Add(new CategoriaVO(5, "Categoria 5", DateTime.Now));
                novosDados.Add(new CategoriaVO(6, "Categoria 6", DateTime.Now));
                novosDados.Add(new CategoriaVO(7, "Categoria 7", DateTime.Now));
                novosDados.Add(new CategoriaVO(8, "Categoria 8", DateTime.Now));
                novosDados.Add(new CategoriaVO(9, "Categoria 9", DateTime.Now));
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

        public static Container<CategoriaVO> GetInstance()
        {
            return instanciaUnica ?? (instanciaUnica = new Categoria2Container());
        }
    }
}
