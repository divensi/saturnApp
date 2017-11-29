using System.Collections.Generic;
using saturnApp.Models;

namespace saturnUpload
{
    public class Diretorio : No
    {
        public virtual List<No> Arquivos { get; set; }

        public Diretorio() : base() {}
        public Diretorio(string Nome, Usuario Dono) : base(Nome, Dono) {}
    }
}