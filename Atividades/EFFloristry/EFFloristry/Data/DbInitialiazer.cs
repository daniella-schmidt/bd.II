using EFFloristry.Models;

namespace EFFloristry.Data
{
    public class DbInitializer
    {
        public static void Initialize(FloristryContext context)
        {
            // Garante que o banco seja criado
            context.Database.EnsureCreated();

            // Se já existem produtos, não inicializa novamente
            if (context.Products.Any())
            {
                return;
            }

            // Dados iniciais mais completos
            var products = new Product[]
            {
                // Flores
                new Product
                {
                    ProductDescription = "Rosa Vermelha Premium",
                    Price = 12.50m, // Usar decimal em vez de float
                    Category = "Flores",
                    Stock = 25
                },
                new Product
                {
                    ProductDescription = "Rosa Branca",
                    Price = 11.90m,
                    Category = "Flores",
                    Stock = 30
                },
                new Product
                {
                    ProductDescription = "Tulipa Amarela",
                    Price = 8.90m,
                    Category = "Flores",
                    Stock = 15
                },
                new Product
                {
                    ProductDescription = "Tulipa Rosa",
                    Price = 9.50m,
                    Category = "Flores",
                    Stock = 12
                },
                new Product
                {
                    ProductDescription = "Orquídea Branca",
                    Price = 35.00m,
                    Category = "Flores",
                    Stock = 8
                },
                new Product
                {
                    ProductDescription = "Orquídea Roxa",
                    Price = 38.50m,
                    Category = "Flores",
                    Stock = 6
                },
                new Product
                {
                    ProductDescription = "Girassol",
                    Price = 6.50m,
                    Category = "Flores",
                    Stock = 20
                },
                new Product
                {
                    ProductDescription = "Lírio Branco",
                    Price = 14.90m,
                    Category = "Flores",
                    Stock = 18
                },

                // Plantas
                new Product
                {
                    ProductDescription = "Samambaia",
                    Price = 22.00m,
                    Category = "Plantas",
                    Stock = 10
                },
                new Product
                {
                    ProductDescription = "Violeta Africana",
                    Price = 15.50m,
                    Category = "Plantas",
                    Stock = 14
                },
                new Product
                {
                    ProductDescription = "Suculenta Echeveria",
                    Price = 8.00m,
                    Category = "Plantas",
                    Stock = 35
                },
                new Product
                {
                    ProductDescription = "Cacto San Pedro",
                    Price = 18.90m,
                    Category = "Plantas",
                    Stock = 7
                },

                // Arranjos
                new Product
                {
                    ProductDescription = "Arranjo de Rosas Mistas",
                    Price = 45.00m,
                    Category = "Arranjos",
                    Stock = 5
                },
                new Product
                {
                    ProductDescription = "Buquê de Noiva",
                    Price = 120.00m,
                    Category = "Arranjos",
                    Stock = 2
                },
                new Product
                {
                    ProductDescription = "Arranjo Condolências",
                    Price = 65.50m,
                    Category = "Arranjos",
                    Stock = 3
                },
                new Product
                {
                    ProductDescription = "Cestinha de Flores",
                    Price = 38.90m,
                    Category = "Arranjos",
                    Stock = 8
                },

                // Vasos
                new Product
                {
                    ProductDescription = "Vaso de Cerâmica Pequeno",
                    Price = 12.90m,
                    Category = "Vasos",
                    Stock = 25
                },
                new Product
                {
                    ProductDescription = "Vaso de Cerâmica Médio",
                    Price = 18.50m,
                    Category = "Vasos",
                    Stock = 20
                },
                new Product
                {
                    ProductDescription = "Vaso de Cerâmica Grande",
                    Price = 28.00m,
                    Category = "Vasos",
                    Stock = 12
                },
                new Product
                {
                    ProductDescription = "Vaso Suspenso Macramê",
                    Price = 24.90m,
                    Category = "Vasos",
                    Stock = 15
                },

                // Acessórios
                new Product
                {
                    ProductDescription = "Terra Adubada 2kg",
                    Price = 8.50m,
                    Category = "Acessórios",
                    Stock = 50
                },
                new Product
                {
                    ProductDescription = "Adubo Orgânico 500g",
                    Price = 12.00m,
                    Category = "Acessórios",
                    Stock = 30
                },
                new Product
                {
                    ProductDescription = "Regador Plástico 1L",
                    Price = 15.90m,
                    Category = "Acessórios",
                    Stock = 18
                },
                new Product
                {
                    ProductDescription = "Borrifador 500ml",
                    Price = 9.90m,
                    Category = "Acessórios",
                    Stock = 22
                },

                // Produtos com estoque baixo para teste
                new Product
                {
                    ProductDescription = "Rosa Azul Rara",
                    Price = 85.00m,
                    Category = "Flores",
                    Stock = 2 // Estoque baixo
                },
                new Product
                {
                    ProductDescription = "Bonsai Ficus",
                    Price = 150.00m,
                    Category = "Plantas",
                    Stock = 1 // Estoque muito baixo
                }
            };

            // Adiciona os produtos ao contexto
            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            // Salva as mudanças
            context.SaveChanges();
        }
    }
}