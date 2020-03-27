using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using Budget;
using BudgetModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            var appId = 793322895;
            
            var o1 = JObject.Parse(System.IO.File.ReadAllText("appsettings.json"));
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(o1["ConnectionStrings"]["DefaultConnection"].Value<string>());
            var context = new ApplicationDbContext(builder.Options);

            for (var page = 1; page < 11; page++)
            {
                var url = $"https://itunes.apple.com/us/rss/customerreviews/page={page}/id={appId}/sortby=mostRecent/json";
                // var url2 = $"https://itunes.apple.com/WebObjects/MZStore.woa/wa/customerReviews?displayable-kind=11&id={appId}&page={page}&sort=4";

                
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                
                var result = client.SendAsync(request).Result.Content;
                var content = result.ReadAsStringAsync().Result;
                var contentJson = Welcome.FromJson(content);

                foreach (var entry in contentJson.Feed.Entry)
                {
                    var review = new AppStoreScraperModels.Review
                    {
                        id = Convert.ToInt64(entry.Id.Label),
                        score = Convert.ToInt32(entry.ImRating.Label),
                        text = entry.Content.Label,
                        title = entry.Title.Label,
                        url = entry.Link.Attributes.Href.AbsoluteUri,
                        userName = entry.Author.Name.Label,
                        userUrl = entry.Author.Uri.Label,
                        version = entry.ImVersion.Label
                    };
                    context.Reviews.Add(review);
                }

                context.SaveChanges();
            }
            
            
            
            Console.WriteLine();
        }
    }
}