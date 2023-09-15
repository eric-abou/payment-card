using Microsoft.EntityFrameworkCore;
using PaymentCard.API.Models;

namespace PaymentCard.API.Data
{
    public class PaymentDbContext: DbContext
    {

        public PaymentDbContext(DbContextOptions opt): base(opt) { }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
