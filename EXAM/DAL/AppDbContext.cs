using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }

}