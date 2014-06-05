using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheContainer
{
    public abstract class Container<T>
    {
 
        private readonly object acesso = new object();

        private IIntervalo<DateTime> intervaloDeTempoDeValidadeDaCarga;

        private List<T> Dados;
 
        private int cargasRealizadas = 0; // Variavel usada para testar a quantidade de cargas
        
        protected Container()
        {
        }
        
        public int CargasRealizadas
        {
            get { return cargasRealizadas; }
        }

        private bool NaoCarregado
        {
            get { return Dados == null; }
        }

        private bool Expirou
        {
            get { return !intervaloDeTempoDeValidadeDaCarga.Contem(DateTime.Now); }
        }

        public List<T> ObterTodos()
        {
            VerificarValidade();

            return Dados.ToList();
        }

        protected void VerificarValidade()
        {
            lock (acesso)
            {
                if (NaoCarregado || Expirou)
                    CarregarDados();
            }
        }

        public void ReCarregarDados()
        {
            lock (acesso)
            {
                CarregarDados();
            }
        }

        private void CarregarDados()
        {
            try
            {
                var novos = ColetarDados();

                RenovarDados(novos);
            }
            catch (Exception ex)
            {
                //Logar ex;

                RenovarTempoDeValidade(TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos);
            }
        }

        private void RenovarTempoDeValidade(long segundos)
        {
            DateTime agora = DateTime.Now;
            DateTime ateFimDaValidade = agora.AddSeconds(segundos);

            intervaloDeTempoDeValidadeDaCarga = Intervalo.DeDatas(agora, ateFimDaValidade);
        }

        private void RenovarDados(List<T> novosDados)
        {
            if (novosDados != null)
            {
                ExecutarRenovacaoDosDados(novosDados);
                ExecutarRenovacaoDosDadosPontoDeExtensao(novosDados);
                RenovarTempoDeValidade(TempoDeValidadeEmSegundos);
            }
            else
            {
                RenovarTempoDeValidade(TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos);
            }

        }

        private void ExecutarRenovacaoDosDados(List<T> novosDados)
        {
            cargasRealizadas++;
            Dados = new List<T>(novosDados);
        }

        protected virtual void ExecutarRenovacaoDosDadosPontoDeExtensao(List<T> novosDados)
        {
        }

        //Valores podem estar definidos em arquivo de configuração.
        public static readonly long UMA_HORA = 60 * 60;
        public static readonly long CINCO_MINUTOS = 60 * 5;

        protected virtual long TempoDeValidadeEmSegundos
        {
            get { return UMA_HORA; }
        }

        protected virtual long TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos
        {
            get { return CINCO_MINUTOS; }
        }
        
        protected abstract List<T> ColetarDados();


        /// <summary>
        /// Métodos e Variáveis Estáticas
        /// </summary>

        protected static Container<T> instanciaUnica = null;
        
    }
}
