using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace ContainerTeste
{
    [TestClass]
    public class ContainerTeste
    {
        [TestMethod]
        public void IndexacaoPersonalizada()
        {
            var p = ProdutoContainerPersonalizado.GetInstance();

            Assert.IsNotNull(p);

            int qtdCargas = p.CargasRealizadas;

            for (int i = 0; i < 6; i++)
            {
                Assert.AreEqual(1, p.ObterPorId(1).Id);
                Assert.AreEqual(2, p.ObterPorId(2).Id);
                Assert.AreEqual(3, p.ObterPorId(3).Id);

                Assert.AreEqual(1, p.ObterPorNome("Produto 1").Id);
                Assert.AreEqual(2, p.ObterPorNome("Produto 2").Id);
                Assert.AreEqual(3, p.ObterPorNome("Produto 3").Id);

                Assert.IsNull(p.ObterPorId(10));
                Assert.IsNull(p.ObterPorNome("XX"));

                Assert.AreEqual(3, p.ObterTodos().Count);

                Thread.Sleep(1000);
            }       

            Assert.AreEqual(qtdCargas + 3, p.CargasRealizadas);
        }

        [TestMethod]
        public void ReCarregarDados()
        {
            var p = ProdutoContainer.GetInstance();

            Assert.AreEqual(9, p.ObterTodos().Count);
            
            int qtdCargas = p.CargasRealizadas;

            p.ReCarregarDados();
            Assert.AreEqual(qtdCargas + 1, p.CargasRealizadas);

            p.ReCarregarDados();
            Assert.AreEqual(qtdCargas + 2, p.CargasRealizadas);

        }

        [TestMethod]
        public void Renovar()
        {
            var p = ProdutoContainer.GetInstance();

            Assert.IsNotNull(p);
            Assert.AreEqual(9, p.ObterTodos().Count);

            int qtdCargas = p.CargasRealizadas;

            Thread.Sleep(2000);

            Assert.AreEqual(9, p.ObterTodos().Count);
            Assert.AreEqual(qtdCargas + 1, p.CargasRealizadas);

            Thread.Sleep(2010);

            Assert.AreEqual(9, p.ObterTodos().Count);
            Assert.AreEqual(qtdCargas + 2, p.CargasRealizadas);
        }

        [TestMethod]
        public void Basico()
        {
            var p = ProdutoContainer.GetInstance();

            Assert.IsNotNull(p);
            
            Assert.AreEqual(1, p.ObterUm(1).Id);
            Assert.AreEqual(2, p.ObterUm(2).Id);
            Assert.AreEqual(3, p.ObterUm(3).Id);
            Assert.AreEqual(4, p.ObterUm(4).Id);
            Assert.AreEqual(5, p.ObterUm(5).Id);
            Assert.AreEqual(6, p.ObterUm(6).Id);
            Assert.AreEqual(7, p.ObterUm(7).Id);
            Assert.AreEqual(8, p.ObterUm(8).Id);
            Assert.AreEqual(9, p.ObterUm(9).Id);

            Assert.IsNull(p.ObterUm(10));

            Assert.AreEqual(9, p.ObterTodos().Count);




            var c = CategoriaContainer.GetInstance();


            Assert.IsNotNull(c);

            Assert.AreEqual(1, c.ObterTodos().First(x => x.Id == 1).Id);
            Assert.AreEqual(2, c.ObterTodos().First(x => x.Id == 2).Id);
            Assert.AreEqual(3, c.ObterTodos().First(x => x.Id == 3).Id);
            Assert.AreEqual(4, c.ObterTodos().First(x => x.Id == 4).Id);
            Assert.AreEqual(5, c.ObterTodos().First(x => x.Id == 5).Id);
            Assert.AreEqual(6, c.ObterTodos().First(x => x.Id == 6).Id);
            Assert.AreEqual(7, c.ObterTodos().First(x => x.Id == 7).Id);
            Assert.AreEqual(8, c.ObterTodos().First(x => x.Id == 8).Id);
            Assert.AreEqual(9, c.ObterTodos().First(x => x.Id == 9).Id);

            Assert.IsNull(c.ObterTodos().FirstOrDefault(x => x.Id == 10));

            Assert.AreEqual(9, c.ObterTodos().Count);
            
        }
        
        [TestMethod]
        public void UnicaInstancia()
        {
            var p1 = ProdutoContainer.GetInstance();
            var p2 = ProdutoContainer.GetInstance();

            Assert.IsNotNull(p1);
            Assert.IsNotNull(p2);
            Assert.AreEqual(p1, p2);
            Assert.IsTrue(p1 == p2);

            var c1 = CategoriaContainer.GetInstance();
            var c2 = CategoriaContainer.GetInstance();

            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.AreEqual(c1, c2);
            Assert.IsTrue(c1 == c2);

        }

        [TestMethod]
        public void MesmoContainerComMesmoTipoDeDado_Erro()
        {
            var c1 = CategoriaContainer.GetInstance();
            var c2 = CategoriaContainer.GetInstance();
            
            //Um determinado tipo de dado só pode ter UM Container
            var c21 = Categoria2Container.GetInstance();
            var c22 = Categoria2Container.GetInstance();
            

            Assert.AreEqual(c1, c2);
            Assert.IsTrue(c1 == c2);

            Assert.AreEqual(c21, c22);
            Assert.IsTrue(c21 == c22);

            Assert.IsTrue(c1 == c21);
            Assert.IsTrue(c1 == c22);

        }

        [TestMethod]
        public void DiferentesContainersComMesmoTipoDeDado()
        {
            var p1 = ProdutoContainer.GetInstance();
            var p2 = ProdutoContainer.GetInstance();

            Assert.AreEqual(9, p2.ObterTodos().Count);
            
            //Tipos diferentes de container podem armazenar o mesmo tipo de dado sem que um interfira no outro.
            var p21 = ProdutoContainerPersonalizado.GetInstance();
            var p22 = ProdutoContainerPersonalizado.GetInstance();
            
            Assert.AreEqual(3, p22.ObterTodos().Count);
            Assert.AreEqual(9, p2.ObterTodos().Count);

            Assert.AreEqual(p1, p2);
            Assert.IsTrue(p1 == p2);

            Assert.AreEqual(p21, p22);
            Assert.IsTrue(p21 == p22);
        }

        [TestMethod]
        public void SimulacaoDeErroNaColetaDosDados()
        {
            var container = ClienteContainer.NovoParaTeste();

            long numeroDeLeituras = 0;

            var inicio = DateTime.Now;

            while (container.CargasRealizadas < 3)
            {
                for (int i = 1; i < 10; i++)
                {
                    numeroDeLeituras++;
                    Assert.AreEqual(i, container.ObterUm(i).Id);
                }
            }

            var fim = DateTime.Now;

            var tempoDecorrido = fim.Subtract(inicio);


            /*
             * Com Erro
             */

            var containerComErro = ClienteContainer.NovoParaTeste();
            
            containerComErro.ErroNaColeta = 2;

            long numeroDeLeiturasComErro = 0;

            var inicioComErro = DateTime.Now;

            while (containerComErro.CargasRealizadas < 3)
            {
                for (int i = 1; i < 10; i++)
                {
                    numeroDeLeiturasComErro++;
                    Assert.AreEqual(i, containerComErro.ObterUm(i).Id);
                }
            }

            var fimComErro = DateTime.Now;

            var tempoDecorridoComErro = fimComErro.Subtract(inicioComErro);

            Assert.IsTrue(numeroDeLeiturasComErro > numeroDeLeituras); //Porque o tempo de execução das leituras é maior em 50% (TempoDeValidadeEmSegundos = 2 e TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos = 1)
            Assert.IsTrue(tempoDecorridoComErro > tempoDecorrido); //Porque quando ocorre um erro a Quantidade de Cargas Realizadas não é incrementado e o tempo de Validede é estendido. Neste caso em mais um segundo (TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos = 1).
        }
    }
}
