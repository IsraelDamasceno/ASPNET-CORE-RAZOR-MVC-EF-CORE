using System.Linq;
using EscolaIsrael.Models;


namespace EscolaIsrael.Data
{
    public class EscolaDbInitializer
    {
     
         public static void Initialize(EscolaContext context)
            {
                context.Database.EnsureCreated();

                if (context.Cursos.Any())
                {
                    return;
                }

                var cursos = new Curso[]
                {
                new Curso { NomeDoCurso="Excel", Descricao="Curso Livre", Valor=800},
                new Curso {  NomeDoCurso="Excel Avançado Vip", Descricao="Curso Livre", Valor=1800}
                };

                foreach (Curso d in cursos)
                {
                    context.Cursos.Add(d);
                }
                context.SaveChanges();
            }
        }
    }
