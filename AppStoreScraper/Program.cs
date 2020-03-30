using System;
using System.Linq;
using System.Net.Http;
using BudgetModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using QuickType;

namespace AppStoreScraper
{
    class Program
    {
        
        static void Main(string[] args)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);
            
            var o1 = JObject.Parse(System.IO.File.ReadAllText("/Users/cavanfarrell/RiderProjects/AppStoreScraper/AppStoreScraper/appsettings.json"));
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(o1["ConnectionStrings"]["DefaultConnection"].Value<string>());
            var context = new ApplicationDbContext(builder.Options);
            
            var appId = 793322895;

            for (var page = 1; page < 11; page++)
            {
                var url =
                    $"https://itunes.apple.com/us/rss/customerreviews/page={page}/id={appId}/sortby=mostRecent/json";

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var result = client.SendAsync(request).Result.Content;
                var content = result.ReadAsStringAsync().Result;
                var contentJson = Welcome.FromJson(content);

                foreach (var entry in contentJson.Feed.Entry)
                {
                    if (DoesReviewExist(context, Convert.ToInt64(entry.Id.Label))) continue; 
                    
                    var review = new AppStoreScraperModels.Review
                    {
                        id = Convert.ToInt64(entry.Id.Label),
                        score = Convert.ToInt32(entry.ImRating.Label),
                        text = entry.Content.Label,
                        title = entry.Title.Label,
                        url = entry.Link.Attributes.Href.AbsoluteUri,
                        userName = entry.Author.Name.Label,
                        userUrl = entry.Author.Uri.Label,
                        version = entry.ImVersion.Label,
                        DateLoaded = DateTime.Now
                    };
                    context.Reviews.Add(review);
                }

                context.SaveChanges();
            }

            Console.WriteLine();
        }

        static bool DoesReviewExist(ApplicationDbContext context, long id)
        {
            return context.Reviews.Any(r => r.id == id);
        }
    }
}