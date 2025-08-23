using EFFloristry.Models;

namespace EFFloristry.Data
{
    public class DbInitializer
    {
        public static void Initialize(FloristryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product{ ProductDescription="Rosa Vermelha", Price=10.50f },
                new Product{ ProductDescription="Tulipa Amarela", Price=7.90f },
                new Product{ ProductDescription="Orquídea Branca", Price=25.00f },
                new Product{ ProductDescription="Girassol", Price=5.50f }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
