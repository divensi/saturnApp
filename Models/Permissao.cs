using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using saturnApp.Models;

namespace saturnApp.Models
{
    public class Permissao
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Arquivo")]
        public long ArquivoId { get; set; }
        public virtual No Arquivo { get; set; }

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        
        public bool Leitura { get; set; }
        public bool Escrita { get; set; }
    }
}