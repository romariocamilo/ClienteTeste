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
        TcpClient conexao;
        NetworkStream sockStream;
        BinaryWriter escreve;
        BinaryReader ler;
        string usuario = "herbert";
        Queue<string> filaMensagensChat = new Queue<string>();
        Queue<string> filaMensagensConexao = new Queue<string>();
        bool autenticado = false;
        public void ConectaServidor()
        {
            bool primeiroAcesso = true;

            while (true)
            {
                try
                {
                    if (conexao == null)
                    {
                        conexao = new TcpClient("10.1.9.184", 8080);
                        Console.WriteLine("Para enviar mensagem para um usuários digite: nome do usuário: conteúdo da mensagem");
                        Console.WriteLine("Para enviar mensagem para todoas usuários digite: all: conteúdo da mensagem");
                        Console.WriteLine("Para solicitar a lista de usuários ativos digite: requisicao: usuarios online");
                        Console.WriteLine("Para limpar o console digite: cls \n");
                    }


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

                            if (mensagem == "autenticado")
                            {
                                Console.WriteLine(usuario + " " + mensagem);
                                autenticado = true;
                            }
                            else
                            {
                                Console.WriteLine("Usuário não autenticado");
                                autenticado = false;
                            }
                        }
                        else
                        {
                            string mensagem = ler.ReadString();

                            if (mensagem == "ping")
                            {
                                filaMensagensConexao.Enqueue(mensagem);
                            }
                            else
                            {
                                filaMensagensChat.Enqueue(mensagem);
                            }

                            if (mensagem != "ping")
                            {
                                Console.WriteLine(filaMensagensChat.Dequeue());
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Servidor Offline");
                        autenticado = false;
                        conexao = null;
                        Thread.Sleep(1000);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Servidor Offline");
                    autenticado = false;
                    conexao = null;
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }
        public void EnviaMensagemChat()
        {
            while (true)
            {
                if (autenticado)
                {

                    if (escreve != null)
                    {
                        string[] mensagem = Console.ReadLine().ToLowerInvariant().Trim().Split(':');

                        if (mensagem.Length == 2)
                        {
                            if (mensagem[0] == "requisicao")
                            {
                                escreve.Write(usuario + "|requisicao|" + mensagem[1] + "|" + usuario);
                            }
                            else
                            {
                                escreve.Write(usuario + "|chat|" + mensagem[1] + "|" + mensagem[0]);
                            }
                        }
                        else if (mensagem[0] == "cls")
                        {
                            LimpaConsole(mensagem[0]);
                        }
                        else
                        {
                            Console.WriteLine("Mensagem fora do padrão");
                        }
                    }
                }
            }
        }
        public void EnviaMensagemConexao()
        {
            while (true)
            {
                if (autenticado)
                {
                    bool primeiro = true;

                    try
                    {
                        if (escreve != null)
                        {
                            if (primeiro == true)
                            {
                                escreve.Write(usuario + "|conexao|pong|" + usuario);
                                primeiro = false;
                            }
                            else
                            {
                                if (filaMensagensConexao.Count > 0)
                                {
                                    escreve.Write(usuario + "|conexao|pong|" + usuario);
                                    filaMensagensConexao.Dequeue();
                                }
                            }
                        }

                        Thread.Sleep(8000);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
        }
        public void LimpaConsole(string mensagem)
        {
            if (mensagem == "cls")
            {
                Console.Clear();
            }

            Console.WriteLine(usuario);
            Console.WriteLine("Para enviar mensagem para um usuários digite: nome do usuário: conteúdo da mensagem");
            Console.WriteLine("Para enviar mensagem para todoas usuários digite: all: conteúdo da mensagem");
            Console.WriteLine("Para solicitar a lista de usuários ativos digite: requisicao: usuarios online");
            Console.WriteLine("Para limpar o console digite: cls \n");
        }

    }
}