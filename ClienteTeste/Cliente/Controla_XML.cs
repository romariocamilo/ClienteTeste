using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClienteTeste.Cliente
{
    public class Controla_XML
    {
        string caminhoArquivo = Environment.CurrentDirectory + "\\clientes.xml";

        public void ExcreveXML()
        {
            if (File.Exists(caminhoArquivo) == false)
            {
                Cliente cliente = new Cliente(1, "usuario", false);

                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Cliente));
                System.IO.FileStream file = System.IO.File.Create(@caminhoArquivo);
                writer.Serialize(file, cliente);
                file.Close();
            }
        }

        //Lê lista de usuário autenticados
        public Cliente LeXML()
        {
            System.Xml.Serialization.XmlSerializer leitura = new System.Xml.Serialization.XmlSerializer(typeof(Cliente));
            System.IO.StreamReader file = new System.IO.StreamReader(caminhoArquivo);
            Cliente cliente = (Cliente)leitura.Deserialize(file);
            file.Close();

            return cliente;
        }
    }
}
