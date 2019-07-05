using Microsoft.EntityFrameworkCore;
using EscolaIsrael.Models;


namespace EscolaIsrael.Data
{
    public class EscolaContext  : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        public EscolaContext(DbContextOptions<EscolaContext> options) : base(options)
        {

        }

    }
}
