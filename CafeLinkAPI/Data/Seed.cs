using CafeLinkAPI.Entities;

namespace CafeLinkAPI.Data;

public class Seed
{   
    private static Coffee RandomizeCoffee(List<CoffeeType> coffeeTypes)
    {
        var random = new Random();
        var coffeeType = coffeeTypes[random.Next(coffeeTypes.Count)];
        var coffee = new Coffee
        {
            Name = "Random Coffee",
            Description = "A random coffee drink",
            Composition = "Random composition",
            Price = random.Next(20, 50) * 1000,
            Type = coffeeType,
            ImageUrl = "http://cialfla.freedomains.dev/image/api/d6615829-823e-41c0-8e3b-ac97cee7e405.jpeg"
        };
        return coffee;
    }
    private static Cafe RandomizeCafe(List<CafeType> cafeTypes, List<CoffeeType> coffeeTypes)
    {
        var random = new Random();
        var cafeType = cafeTypes.OrderBy(x => random.Next()).Take(2);
        var cafe = new Cafe
        {
            Name = "Random Cafe",
            Description = "A random cafe",
            ImageUrl = "http://cialfla.freedomains.dev/image/api/8d22c0bf-8222-4116-ae83-73cd6c78df33.png",
            Types = cafeType.ToList(),
            Coffees = new List<Coffee> { RandomizeCoffee(coffeeTypes), RandomizeCoffee(coffeeTypes), RandomizeCoffee(coffeeTypes) },
            PriceAverage = random.Next(20, 50) * 1000,
            DistrictAddress = "Random District",
            CityAddress = "Random City",
            ProvinceAddress = "Random Province",
            FullAddress = "Random Address",
            Specials = new List<string> { "Random Special 1", "Random Special 2" }
        };
        return cafe;
    }
    public static async Task FakeCafeAndCoffee(DataContext context)
    {
        context.Database.BeginTransaction();
        try {
            var cafes = new List<Cafe>();
            var cafeTypes = context.CafeTypes.ToList();
            var coffeeTypes = context.CoffeeTypes.ToList();
            for (int i = 0; i < 8; i++)
            {
                cafes.Add(RandomizeCafe(cafeTypes, coffeeTypes));
            }
            await context.Cafes.AddRangeAsync(cafes);
            await context.SaveChangesAsync();
            context.Database.CommitTransaction();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            context.Database.RollbackTransaction();
        }
    }
    public static async Task SeedCafeAndCoffee(DataContext context)
    {
        //begin transaction
        context.Database.BeginTransaction();
        try {
            var cafeTypes = new List<CafeType>
            {
                new CafeType { Name = "Industrial" },
                new CafeType { Name = "Tropical" },
                new CafeType { Name = "Modern" },
                new CafeType { Name = "Homely" },
                new CafeType { Name = "Warm" },
                new CafeType { Name = "Rustic" },
                new CafeType { Name = "Airy" },
            }; 
            // save the cafe types to the database
                await context.CafeTypes.AddRangeAsync(cafeTypes);
                await context.SaveChangesAsync();
            var coffeeTypes = new List<CoffeeType>
            {
                new CoffeeType { Name = "Espresso-based" },
                new CoffeeType { Name = "Tea-based" },
            };
            // save the coffee types to the database
            await context.CoffeeTypes.AddRangeAsync(coffeeTypes);
                    // ImageUrl = "http://cialfla.freedomains.dev/image/api/41c22887-a148-4e15-b6f5-e9f75ed3ab4d.jpeg",

            await context.SaveChangesAsync();

            var cafes = new List<Cafe>
            {
                new Cafe
                {
                    Name = "Kopi Lima Detik",
                    Description = "Nestled in the heart of Kemang, Kopi Lima Detik offers a modern industrial vibe with a touch of tropical ambiance. Perfect for those seeking a creative yet relaxing environment, this café combines great coffee with an atmosphere that inspires productivity and leisure.",
                    ImageUrl = "http://cialfla.freedomains.dev/image/api/73ce2652-e4c4-44ec-a98c-e3410dc2f1c8.png",
                    Types = new List<CafeType> { cafeTypes[0], cafeTypes[1] },
                    Coffees = new List<Coffee>
                    {
                        new Coffee
                        {
                            Name = "Capuccino",
                            Description = "Cappuccino is a coffee drink that is made with espresso and steamed milk. It is a rich and creamy drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/41c22887-a148-4e15-b6f5-e9f75ed3ab4d.jpeg",
                            Composition = "Coffee 50% | Milk 50%",
                            Price = 30000,
                            Type = coffeeTypes[0]
                        },
                        new Coffee
                        {
                            Name = "Chai Latte",
                            Description = "Chai latte is a spiced tea concentrate with milk. It is a sweet and spicy drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/5874e3d7-561a-44d3-8d2b-68f5f87e1043.jpeg",
                            Composition = "Tea 60% | Milk 40%",
                            Price = 25000,
                            Type = coffeeTypes[1],
                        },
                        new Coffee
                        {
                            Name = "Macchiato",
                            Description = "A macchiato is a shot of espresso with a small amount of milk. It is a strong and bold coffee drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/7865df75-16ce-4ab6-8859-586403c21d8f.jpeg",
                            Composition = "Coffee 80% | Milk 20%",
                            Price = 40000,
                            Type = coffeeTypes[0],
                        }
                    },
                    PriceAverage = 35000,
                    DistrictAddress = "Kemang",
                    CityAddress = "Jakarta Selatan",
                    ProvinceAddress = "DKI Jakarta",
                    FullAddress = "Kemang, Jakarta Selatan.",
                    Specials = new List<string> { "Free Wi-Fi", "Live acoustic sessions on weekends", "Outdoor Seating" }
                },
                new Cafe
                {
                    Name = "Twenty Fifth Coffee",
                    Description = "Twenty Fifth Coffee is a hidden gem in the bustling area of Kebayoran Baru. With a homely interior and modern design, it's the perfect spot to unwind after a busy day or meet friends.",
                    ImageUrl = "http://cialfla.freedomains.dev/image/api/d484e296-4631-42e3-b450-2f60e1e2ff95.png",
                    Types = new List<CafeType> { cafeTypes[2], cafeTypes[3] },
                    Coffees = new List<Coffee> 
                    {
                        new Coffee
                        {
                            Name = "Macchiato",
                            Description = "A macchiato is a shot of espresso with a small amount of milk. It is a strong and bold coffee drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/7865df75-16ce-4ab6-8859-586403c21d8f.jpeg",
                            Composition = "Coffee 80% | Milk 20%",
                            Price = 35000,
                            Type = coffeeTypes[0],
                        },
                        new Coffee
                        {
                            Name = "Espresso",
                            Description = "Espresso is a strong and bold coffee drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/1bd44935-0e97-4e88-9b66-0e5026006be0.jpeg",
                            Composition = "Coffee 100%",
                            Price = 20000,
                            Type = coffeeTypes[0],
                        },
                        new Coffee
                        {
                            Name = "Capuccino",
                            Description = "Cappuccino is a coffee drink that is made with espresso and steamed milk. It is a rich and creamy drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/41c22887-a148-4e15-b6f5-e9f75ed3ab4d.jpeg",
                            Composition = "Coffee 50% | Milk 50%",
                            Price = 30000,
                            Type = coffeeTypes[0]
                        }
                    },
                    PriceAverage = 40000,
                    DistrictAddress = "Kebayoran Baru",
                    CityAddress = "Jakarta Selatan",
                    ProvinceAddress = "DKI Jakarta",
                    FullAddress = "Kebayoran Baru, Jakarta Selatan.",
                    Specials = new List<string> { "Cozy seating for small groups", "Handpicked coffee beans sourced from local farmers", "Dessert pairing options available" }
                },
                new Cafe
                {
                    Name = "Titik Temu Coffee",
                    Description = "Titik Temu Coffee blends local culture with a warm, wooden ambiance that makes you feel instantly at home. This café is a favorite for those who appreciate tranquility and great coffee.",
                    ImageUrl = "http://cialfla.freedomains.dev/image/api/24f44e96-4eaf-42bc-9192-77d02a822023.png",
                    Types = new List<CafeType> { cafeTypes[4], cafeTypes[5] },
                    Coffees = new List<Coffee>
                    {
                        new Coffee
                        {
                            Name = "Capuccino",
                            Description = "Cappuccino is a coffee drink that is made with espresso and steamed milk. It is a rich and creamy drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/41c22887-a148-4e15-b6f5-e9f75ed3ab4d.jpeg",
                            Composition = "Coffee 50% | Milk 50%",
                            Price = 30000,
                            Type = coffeeTypes[0]
                        },
                        new Coffee
                        {
                            Name = "Macchiato",
                            Description = "A macchiato is a shot of espresso with a small amount of milk. It is a strong and bold coffee drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/7865df75-16ce-4ab6-8859-586403c21d8f.jpeg",
                            Composition = "Coffee 80% | Milk 20%",
                            Price = 35000,
                            Type = coffeeTypes[0],
                        },
                        new Coffee
                        {
                            Name = "Chai Latte",
                            Description = "Chai latte is a spiced tea concentrate with milk. It is a sweet and spicy drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/5874e3d7-561a-44d3-8d2b-68f5f87e1043.jpeg",
                            Composition = "Tea 60% | Milk 40%",
                            Price = 25000,
                            Type = coffeeTypes[1],
                        }
                    },
                    PriceAverage = 30000,
                    DistrictAddress = "Cilandak",
                    CityAddress = "Jakarta Selatan",
                    ProvinceAddress = "DKI Jakarta",
                    FullAddress = "Cilandak, Jakarta Selatan.",
                    Specials = new List<string> { "Quiet corners for work or study", "Locally inspired pastries", "Eco-friendly packaging" }
                },
                new Cafe
                {
                    Name = "Tanatap",
                    Description = "Surrounded by greenery, Tanatap offers a tropical escape in the middle of the city. Known for its airy design and refreshing atmosphere, this café is perfect for relaxation.",
                    ImageUrl = "http://cialfla.freedomains.dev/image/api/466da442-044c-4d87-9fa6-d3767856543b.png",
                    Types = new List<CafeType> { cafeTypes[6], cafeTypes[1] },
                    Coffees = new List<Coffee>
                    {
                        new Coffee
                        {
                            Name = "Espresso",
                            Description = "Espresso is a strong and bold coffee drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/1bd44935-0e97-4e88-9b66-0e5026006be0.jpeg",
                            Composition = "Coffee 100%",
                            Price = 20000,
                            Type = coffeeTypes[0],
                        },
                        new Coffee
                        {
                            Name = "Chai Latte",
                            Description = "Chai latte is a spiced tea concentrate with milk. It is a sweet and spicy drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/5874e3d7-561a-44d3-8d2b-68f5f87e1043.jpeg",
                            Composition = "Tea 60% | Milk 40%",
                            Price = 25000,
                            Type = coffeeTypes[1],
                        },
                        new Coffee
                        {
                            Name = "Macchiato",
                            Description = "A macchiato is a shot of espresso with a small amount of milk. It is a strong and bold coffee drink that is perfect for those who want a little bit of everything.",
                            ImageUrl = "http://cialfla.freedomains.dev/image/api/7865df75-16ce-4ab6-8859-586403c21d8f.jpeg",
                            Composition = "Coffee 80% | Milk 20%",
                            Price = 35000,
                            Type = coffeeTypes[0],
                        }
                    },
                    PriceAverage = 30000,
                    DistrictAddress = "Cilandak",
                    CityAddress = "Jakarta Selatan",
                    ProvinceAddress = "DKI Jakarta",
                    FullAddress = "Cilandak, Jakarta Selatan.",
                    Specials = new List<string> { "Open-air seating", "Pet-friendly zone", "Artisanal baked goods" }
                }
            };

            // save the cafes to the database
            await context.Cafes.AddRangeAsync(cafes);
            await context.SaveChangesAsync();
            //commit transaction
            context.Database.CommitTransaction();
        } catch (Exception ex) {
            //rollback transaction
            context.Database.RollbackTransaction();
            Console.WriteLine(ex.Message);
        }
    }
}