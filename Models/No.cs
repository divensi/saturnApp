using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using saturnApp.Models;

namespace saturnUpload
{
    public abstract class No
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
        public long? PaiId { get; set;}
        public virtual No Pai { get; set; }
        //public string DonoId { get; set; }
        //public virtual Usuario Dono { get; set; }
        public virtual List<Permissao> Permissoes { get; set; }

        public No(string Nome, Usuario Dono) {
            this.Nome = Nome;
            this.Pai = null;
        //    this.Dono = Dono;
        }

        public No()
        {
        }
    }
}