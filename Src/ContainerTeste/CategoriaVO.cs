using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheContainer;

namespace ContainerTeste
{
    public class CategoriaVO //: IContainerKey<int>
    {

        public CategoriaVO(int id, string nome, DateTime data)
        {
            this.id = id;
            this.nome = nome;
            this.data = data;
        }

        //Necessário para ser indexado.
        public int GetKey()
        {
            return id;
        }

        private readonly int id;
        public int Id
        {
            get { return id; }
        }


        private readonly string nome;
        public string Nome
        {
            get { return nome; }
        }

        private readonly DateTime data;
        public DateTime Data
        {
            get { return data; }
        }
    }
}
