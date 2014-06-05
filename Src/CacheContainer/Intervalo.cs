using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheContainer
{
    public static class Intervalo
    {
        public static IIntervalo<DateTime> DeDatas(DateTime inicio, DateTime fim)
        {
            return Intervalo<DateTime>.Novo(inicio, fim);
        }
    }

    public interface IIntervalo<T> where T : IComparable<T>
    {
        T Inicio { get; }
        T Fim { get; }
        bool Contem(T candidato);
    }

    public class Intervalo<T> : IIntervalo<T> where T : IComparable<T>
    {
        
        private Intervalo(T inicio, T fim)
        {
            this.inicio = inicio;
            this.fim = fim;
        }

        private readonly T inicio;
        public T Inicio
        {
            get { return inicio; }
        }

        private readonly T fim;
        public T Fim
        {
            get { return fim; }
        }

        public bool Contem(T candidato)
        {
            if (Inicio.CompareTo(candidato) > 0
                || Fim.CompareTo(candidato) < 0)
                return false;

            return true;
        }

        public static Intervalo<T> Novo<T>(T inicio, T fim) where T : IComparable<T>
        {
            return new Intervalo<T>(inicio, fim);
        }

    }
}
