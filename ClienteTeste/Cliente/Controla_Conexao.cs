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
        string usuario;
        Queue<string> filaMensagensChat = new Queue<string>();
        Queue<string> filaMensagensConexao = new Queue<string>();
        bool logado = false;
        bool primeiroAcesso;
        bool primeiraMensagem;
        //public void ConectaServidor()
        //{
        //    bool primeiroAcesso = true;

        //    while (true)
        //    {
        //        try
        //        {
        //            if (conexao == null)
        //            {
        //                conexao = new TcpClient("192.168.100.5", 8080);
        //                Console.WriteLine("Para enviar mensagem para um usuários digite: nome do usuário: conteúdo da mensagem");
        //                Console.WriteLine("Para enviar mensagem para todoas usuários digite: all: conteúdo da mensagem");
        //                Console.WriteLine("Para solicitar a lista de usuários ativos digite: requisicao: usuarios online");
        //                Console.WriteLine("Para limpar o console digite: cls \n");
        //            }


        //            sockStream = conexao.GetStream();
        //            escreve = new BinaryWriter(sockStream);
        //            ler = new BinaryReader(sockStream);

        //            try
        //            {
        //                if (primeiroAcesso)
        //                {
        //                    escreve.Write("login|" + usuario);
        //                    primeiroAcesso = false;
        //                    string mensagem = ler.ReadString();

        //                    if (mensagem == "autenticado")
        //                    {
        //                        Console.WriteLine(usuario + " " + mensagem);
        //                        autenticado = true;
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("Usuário não autenticado");
        //                        autenticado = false;
        //                    }
        //                }
        //                else
        //                {
        //                    string mensagem = ler.ReadString();

        //                    if (mensagem == "ping")
        //                    {
        //                        filaMensagensConexao.Enqueue(mensagem);
        //                    }
        //                    else
        //                    {
        //                        filaMensagensChat.Enqueue(mensagem);
        //                    }

        //                    if (mensagem != "ping")
        //                    {
        //                        Console.WriteLine(filaMensagensChat.Dequeue());
        //                    }
        //                }
        //            }
        //            catch
        //            {
        //                Console.WriteLine("Servidor Offline");
        //                autenticado = false;
        //                conexao = null;
        //                Thread.Sleep(1000);
        //                continue;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Servidor Offline");
        //            autenticado = false;
        //            conexao = null;
        //            Thread.Sleep(1000);
        //            continue;
        //        }
        //    }
        //}
        //public void EnviaMensagemChat()
        //{
        //    while (true)
        //    {
        //        if (autenticado)
        //        {

        //            if (escreve != null)
        //            {
        //                string[] mensagem = Console.ReadLine().ToLowerInvariant().Trim().Split(':');

        //                if (mensagem.Length == 2)
        //                {
        //                    if (mensagem[0] == "requisicao")
        //                    {
        //                        escreve.Write(usuario + "|requisicao|" + mensagem[1] + "|" + usuario);
        //                    }
        //                    else
        //                    {
        //                        escreve.Write(usuario + "|chat|" + mensagem[1] + "|" + mensagem[0]);
        //                    }
        //                }
        //                else if (mensagem[0] == "cls")
        //                {
        //                    LimpaConsole(mensagem[0]);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Mensagem fora do padrão");
        //                }
        //            }
        //        }
        //    }
        //}
        //public void EnviaMensagemConexao()
        //{
        //    while (true)
        //    {
        //        if (autenticado)
        //        {
        //            bool primeiro = true;

        //            try
        //            {
        //                if (escreve != null)
        //                {
        //                    if (primeiro == true)
        //                    {
        //                        escreve.Write(usuario + "|conexao|pong|" + usuario);
        //                        primeiro = false;
        //                    }
        //                    else
        //                    {
        //                        if (filaMensagensConexao.Count > 0)
        //                        {
        //                            escreve.Write(usuario + "|conexao|pong|" + usuario);
        //                            filaMensagensConexao.Dequeue();
        //                        }
        //                    }
        //                }

        //                Thread.Sleep(8000);
        //            }
        //            catch (Exception ex)
        //            {
        //                continue;
        //            }
        //        }
        //    }
        //}
        //public void LimpaConsole(string mensagem)
        //{
        //    if (mensagem == "cls")
        //    {
        //        Console.Clear();
        //    }

        //    Console.WriteLine(usuario);
        //    Console.WriteLine("Para enviar mensagem para um usuários digite: nome do usuário: conteúdo da mensagem");
        //    Console.WriteLine("Para enviar mensagem para todoas usuários digite: all: conteúdo da mensagem");
        //    Console.WriteLine("Para solicitar a lista de usuários ativos digite: requisicao: usuarios online");
        //    Console.WriteLine("Para limpar o console digite: cls \n");
        //}
        public void ConectaServidor()
        {
            try
            {
                Controla_XML oControla_XML = new Controla_XML();

                primeiroAcesso = true;
                logado = false;
                //Console.WriteLine("Digite o usuário: ");
                //string usuarioFixo = Console.ReadLine();

                while (true)
                {
                    oControla_XML.ExcreveXML();
                    usuario = oControla_XML.LeXML().Apelido;
                    //Console.WriteLine("Conecta servidor rodando");
                    try
                    {
                        if (primeiroAcesso)
                        {
                            conexao = new TcpClient("10.1.9.184", 8080);
                            sockStream = conexao.GetStream();
                            escreve = new BinaryWriter(sockStream);
                            ler = new BinaryReader(sockStream);

                            escreve.Write("login|" + usuario);
                            primeiroAcesso = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Servidor offline");
                        Console.WriteLine("Tentando reconexão...");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Estourou no ConectaServidor");
            }
        }
        public void EscutaMensagem()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(100);
                    //Console.WriteLine("EscutaMensagem rodando");
                    try
                    {
                        string mensagemServidor;

                        if (ler != null)
                        {
                            mensagemServidor = ler.ReadString();

                            if (mensagemServidor.ToLowerInvariant() == "autenticado" && usuario != null)
                            {
                                logado = true;
                                Console.Clear();
                                Console.WriteLine(usuario + " autenticado \n");

                                Console.WriteLine("Para enviar mensagem para um usuários digite: nome do usuário: conteúdo da mensagem");
                                Console.WriteLine("Para enviar mensagem para todoas usuários digite: all: conteúdo da mensagem");
                                Console.WriteLine("Para solicitar a lista de usuários ativos digite: requisicao: usuarios online");
                                Console.WriteLine("Para limpar o console digite: cls \n");
                            }
                            else if (mensagemServidor.ToLowerInvariant() == "ping")
                            {
                                filaMensagensConexao.Enqueue(mensagemServidor);
                            }
                            else
                            {
                                filaMensagensChat.Enqueue(mensagemServidor);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ler = null;
                        continue;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Estourou no EscutaMensagem");
            }
        }
        public void TestaConexao()
        {
            try
            {
                primeiraMensagem = true;

                while (true)
                {
                    if (logado)
                    {
                        Thread.Sleep(1000);
                        // Console.WriteLine("TestaConexao rodando");
                        try
                        {
                            if (escreve != null)
                            {
                                if (primeiraMensagem == true && usuario != null)
                                {
                                    escreve.Write(usuario + "|conexao|pong|" + usuario);
                                    primeiraMensagem = false;
                                }

                                if (logado == true && filaMensagensConexao.Count > 0 && usuario != null)
                                {
                                    filaMensagensConexao.Dequeue();
                                    escreve.Write(usuario + "|conexao|pong|" + usuario);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            primeiroAcesso = true;
                            primeiraMensagem = true;
                            usuario = null;
                            if (conexao != null)
                            {
                                conexao = null;
                                sockStream = null;
                                ler = null;
                                escreve = null;
                            }
                            continue;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Estourou no TestaConexao");
            }
        }
        public void DescarregaMensagensChat()
        {
            try
            {
                while (true)
                {
                    if (logado)
                    {
                        Thread.Sleep(100);
                        //Console.WriteLine("DescarregaMensagensChat rodando");

                        if (logado == true && filaMensagensChat.Count > 0)
                        {
                            Console.WriteLine(filaMensagensChat.Dequeue());
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Estourou no DescarregaMensagensChat");
            }
        }
        public void EscreveMensagem()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(100);
                    //Console.WriteLine("EscreveMensagem rodando");
                    if (logado)
                    {
                        if (escreve != null && usuario != null)
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
            catch
            {
                Console.WriteLine("Estourou no EscreveMensagem");
            }
        }
        public void LimpaConsole(string mensagem)
        {
            if (mensagem == "cls")
            {
                Console.Clear();
            }

            Console.WriteLine(usuario + "\n");
            Console.WriteLine("Para enviar mensagem para um usuários digite: nome do usuário: conteúdo da mensagem");
            Console.WriteLine("Para enviar mensagem para todoas usuários digite: all: conteúdo da mensagem");
            Console.WriteLine("Para solicitar a lista de usuários ativos digite: requisicao: usuarios online");
            Console.WriteLine("Para limpar o console digite: cls \n");
        }
    }
}