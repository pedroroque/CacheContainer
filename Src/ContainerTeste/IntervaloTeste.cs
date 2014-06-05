using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CacheContainer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerTeste
{
    [TestClass]
    public class IntervaloTeste
    {

        [TestMethod]
        public void Basico()
        {
            var inicio = new DateTime(2014, 01, 01);
            var fim = new DateTime(2014, 01, 02);

            var intervalo = Intervalo.DeDatas(inicio, fim);

            Assert.IsNotNull(intervalo);
            Assert.AreEqual(inicio, intervalo.Inicio);
            Assert.AreEqual(fim, intervalo.Fim);

            Assert.IsTrue(intervalo.Contem(inicio));
            Assert.IsTrue(intervalo.Contem(fim));

            Assert.IsFalse(intervalo.Contem(inicio.AddMilliseconds(-1)));
            Assert.IsFalse(intervalo.Contem(fim.AddMilliseconds(1)));

        }
    }
}
