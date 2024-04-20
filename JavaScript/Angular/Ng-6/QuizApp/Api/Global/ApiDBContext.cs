using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Global
{
    public class ApiDBContext: DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options): base(options) {}

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
