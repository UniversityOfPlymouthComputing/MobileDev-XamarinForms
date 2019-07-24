using System;
using System.Collections.Generic;

namespace EnumeratedTypes
{
    public static class Extensions
    {
        public static string Definition(this MarketSector s)
        {
            switch (s)
            {
                case MarketSector.Omnivore:
                    return "Eats both meat and vegetables";
                case MarketSector.Vegetarian:
                    return "A person who does not eat meat or fish, and sometimes other animal products, especially for moral, religious, or health reasons.";
                case MarketSector.Vegan:
                    return "A person who does not eat or use animal products.";
                case MarketSector.Fruitarian:
                    return "A person who eats only fruit, and possibly nuts and seeds.";
                default:
                    return "Error - did you add another category without updating the code?";
            }
        }
    }
    public enum MarketSector
    {
        Omnivore=1, Vegetarian=2, Vegan=Vegetarian+10, Fruitarian = Vegetarian +100
    }

    [Flags]
    public enum IngredientsContain
    {
        Wheat = 1,
        Dairy = 2,
        Gluten = 4,
        Nuts = 8
    }

    class Recipe
    {
        public IList<string> Ingredients { get; set; }
        public IngredientsContain Allergens { get; set; }
        public MarketSector TargetMarket;

        public string PackagingNoticeSuitability
        {
            get
            {
                switch (TargetMarket)
                {
                    case MarketSector.Omnivore:
                        return "Contains meat products";
                    case MarketSector.Vegetarian:
                        return "Suitable for Vegetarians";
                    case MarketSector.Vegan:
                        return "Vegan";
                    case MarketSector.Fruitarian:
                        return "Fruitarian Certified";
                    default:
                        return "Note sure - don't eat this!";
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MarketSector s = MarketSector.Vegan;
            Console.WriteLine((int)s);

            Recipe FavCurry = new Recipe();

            FavCurry.Ingredients = new List<string>{
                "Potato",
                "Rice",
                "Hot Chilli Powder",
                "Onions",
                "Yoghurt"
            };

            FavCurry.TargetMarket = MarketSector.Vegetarian;
            FavCurry.Allergens = IngredientsContain.Dairy | IngredientsContain.Gluten;

            Console.WriteLine($"Target Market Category {(int)FavCurry.TargetMarket} : " + FavCurry.TargetMarket);
            Console.WriteLine(FavCurry.Allergens);
            Console.WriteLine(FavCurry.PackagingNoticeSuitability);
            
            if (FavCurry.Allergens.HasFlag(IngredientsContain.Nuts))
            {
                Console.WriteLine("Warning - this product may be banned from certain airlines");
            }

            Console.WriteLine(FavCurry.TargetMarket.Definition());
        }
    }
}
