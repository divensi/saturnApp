using System;
using saturnApp.Models;

namespace saturnApp.Models
{
    public class Arquivo : No
    {
        public string Endereco { get; set; }
        public string Hash { get; set; }
        public long Tamanho { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public Arquivo(string Nome, Usuario Dono) : base(Nome, Dono) {}

        public Arquivo() : base() {}
    }
}