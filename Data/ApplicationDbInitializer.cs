using saturnApp.Models;
using System.Linq;

namespace saturnApp.Data
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();

            // if (context.Departamentos.Any())
            // {
            //     //    return;
            // }


            // var instituicoes = new Instituicao[]
            // {
            //     new Instituicao()
            //     {
            //         Nome = "UTFPR",
            //         Endereco = "Medianeira"
            //     },
            //     new Instituicao()
            //     {
            //         Nome = "UDC",
            //         Endereco = "Foz"
            //     },
            //     new Instituicao()
            //     {
            //         Nome = "Uniao",
            //         Endereco = "Matelandia"
            //     }
            // };

            // foreach (Instituicao i in instituicoes)
            // {
            //     context.Instituicoes.Add(i);
            // }

            // context.SaveChanges();

            // Instituicao instit = context.Instituicoes.FirstOrDefault();

            // var departamentos = new Departamento[]
            // {
            //     new Departamento {Nome="Ciencia da Computacao", InstituicaoID=instit.InstituicaoID},
            //     new Departamento {Nome="Engenharia Ambiental", InstituicaoID=instit.InstituicaoID}
            // };

            // foreach (Departamento d in departamentos)
            // {
            //     context.Departamentos.Add(d);
            // }

            //context.SaveChanges();
        }
    }
}