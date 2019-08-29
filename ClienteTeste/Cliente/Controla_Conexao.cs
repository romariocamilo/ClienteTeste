using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClienteTeste.Cliente
{
    public class Controla_Conexao
    {
        NetworkStream sockStream;
        BinaryWriter escreve;
        BinaryReader ler;
        string usuario = "ricardo";
        //Queue<string> filaMensagensChat = new Queue<string>();
        //Queue<string> filaMensagensConexao = new Queue<string>();
        public void ConectaServidor()
        {
            bool primeiroAcesso = true;
            TcpClient conexao = new TcpClient("127.0.0.1", 8080);

            while (true)
            {
                sockStream = conexao.GetStream();
                escreve = new BinaryWriter(sockStream);
                ler = new BinaryReader(sockStream);

                try
                {
                    if (primeiroAcesso)
                    {
                        escreve.Write("login|" + usuario);
                        primeiroAcesso = false;
                        string mensagem = ler.ReadString();
                        Console.WriteLine(mensagem);
                    }
                    else
                    {
                        //Console.WriteLine("Aguardando mensagem do servidor");
                        string mensagem = ler.ReadString();
                        if (mensagem != "ping")
                        {
                            Console.WriteLine(mensagem);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Login Inválido");
                }
            }
        }

        public void EnviaMensagemChat()
        {
            Console.WriteLine("Digite a mensagem: destinátario|conteúdo");
            while (true)
            {
                if (escreve != null)
                {
                    string[] mensagem = Console.ReadLine().Split('|');

                    escreve.Write(usuario + "|chat|" + mensagem[1] + "|" + mensagem[0]);
                }
            }
        }
        public void EnviaMensagemConexao()
        {
            while (true)
            {
                if (escreve != null)
                {
                    escreve.Write(usuario + "|conexao|pong|" + usuario);
                }

                Thread.Sleep(8000);
            }
        }
    }
}