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

            Thread ligaServidor = new Thread(new ThreadStart(objetoCC.ConectaServidor));
            ligaServidor.Start();

            Thread.Sleep(2000);
            Thread enviaMensagem = new Thread(new ThreadStart(objetoCC.EnviaMensagemChat));
            enviaMensagem.Start();

            Thread enviaMensagemConexao = new Thread(new ThreadStart(objetoCC.EnviaMensagemConexao));
            enviaMensagemConexao.Start();
        }
    }
}
