using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheContainer
{
    public abstract class ContainerIndexado<K, T> : Container<T>
        where K : IEquatable<K>
        where T : IContainerKey<K>
    {
        private Dictionary<K, T> DadosIndexados;
 

        public T ObterUm(K key)
        {
            VerificarValidade();

            if (DadosIndexados.ContainsKey(key))
                return DadosIndexados[key];

            return default(T);
        }

        protected override void ExecutarRenovacaoDosDadosPontoDeExtensao(List<T> novosDados)
        {
            DadosIndexados = new Dictionary<K, T>();

            foreach (var valor in novosDados)
            {
                 DadosIndexados.Add(valor.GetKey(), valor);
            }
        }

       
        /// <summary>
        /// Métodos e Variáveis Estáticas
        /// </summary>

        protected new static ContainerIndexado<K, T> instanciaUnica = null;
        
    }
}
