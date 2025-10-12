using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GYMManagementDL.Data.DataSeeding
{
    public static class GymManagementDbContextSeeding
    {
        public static bool IsSeeding(GymManagementDbContext dbContext)
        {
            try
            {
                var HasPlan=dbContext.Plans.Any();
                var HasCategories=dbContext.Categories.Any();

                if (!HasPlan)
                {
                    var plans = LoadDataFromFile<Plan>("plans.json");
                    if(plans.Any() ) dbContext.AddRange(plans);
                }
                if (!HasCategories) 
                {
                    var categories = LoadDataFromFile<Category>("categories.json");
                    if(categories.Any() ) dbContext.AddRange(categories);

                }
                return dbContext.SaveChanges()>0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

      private static List<T> LoadDataFromFile<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\",fileName);

            if (!File.Exists(filePath)) throw new FileNotFoundException();

            string data = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();

        }
    }
}
