using System;
using System.Threading;
using ClienteTeste.Cliente;

namespace ClienteTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            Controla_Conexao objetoCC = new Controla_Conexao();

            Thread conectaServidor = new Thread(new ThreadStart(objetoCC.ConectaServidor));
            conectaServidor.Name = "Conecta no Servidor";
            conectaServidor.Start();

            Thread.Sleep(3000);
            Thread escutaMenagens = new Thread(new ThreadStart(objetoCC.EscutaMensagem));
            escutaMenagens.Name = "Escuta Mensagens";
            escutaMenagens.Start();

            Thread testaConexao = new Thread(new ThreadStart(objetoCC.TestaConexao));
            testaConexao.Name = "Testa Conexão";
            testaConexao.Start();

            Thread descarregaMensagens = new Thread(new ThreadStart(objetoCC.DescarregaMensagensChat));
            descarregaMensagens.Name = "Descarrega Mensagens";
            descarregaMensagens.Start();

            Thread escreveMensagens = new Thread(new ThreadStart(objetoCC.EscreveMensagem));
            escreveMensagens.Name = "Escreve Mensagens";
            escreveMensagens.Start();
        }
    }
}
