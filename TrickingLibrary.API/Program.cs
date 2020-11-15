using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrickingLibrary.Data;
using TrickingLibrary.Models;
using TrickingLibrary.Models.Moderation;

namespace TrickingLibrary.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    ctx.Add(new Difficutly
                        {Id = "easy", Name = "Easy", Description = "Easy Description 1"});
                    ctx.Add(new Difficutly
                        {Id = "medium", Name = "Medium", Description = "Medium Description 2"});
                    ctx.Add(new Difficutly
                        {Id = "hard", Name = "Hard", Description = "Hard Description 3"});
                    ctx.Add(new Category
                        {Id = "kick", Name = "Kick", Description = "Kick Description 2"});
                    ctx.Add(new Category
                        {Id = "flip", Name = "Flip", Description = "Flip Description 2"});
                    ctx.Add(new Category
                        {Id = "transition", Name = "Transition", Description = "Transition Description 2"});
                    ctx.Add(new Trick()
                    {
                        Id = "backwards-roll", 
                        Name = "Backwards Roll", 
                        Description = "This is a test Backwards Roll", 
                        Difficulty = "easy",
                        TrickCategories = new List<TrickCategory>(){ new TrickCategory() { CategoryId = "flip"}}
                    });
                    ctx.Add(new Trick()
                    {
                        Id = "forwards-roll", 
                        Name = "Forwards Roll", 
                        Description = "This is a test Forwards Roll", 
                        Difficulty = "easy",
                        TrickCategories = new List<TrickCategory>(){ new TrickCategory() { CategoryId = "flip"}}
                    });
                    ctx.Add(new Trick()
                    {
                        Id = "back-flip", 
                        Name = "Back Flip", 
                        Description = "This is a test back flip", 
                        Difficulty = "medium",
                        TrickCategories = new List<TrickCategory>(){ new TrickCategory() { CategoryId = "flip"}},
                        Prerequisites = new List<TrickRelationship>()
                        {
                            new TrickRelationship() {PrerequisiteId = "backwards-roll"}
                        }
                    });
                    ctx.Add(new Submission()
                    {
                        TrickId = "back-flip",
                        Description = "Test description, I've tried to go for max height",
                        Video = new Video
                        {
                            VideoLink = "c637408860847911824.mp4",
                            ThumbLink = "t637408860847912445.png"
                        },
                        VideoProcessed = true
                    });
                    ctx.Add(new ModerationItem()
                    {
                        Target = "backwards-roll",
                        Type = ModerationType.Trick
                    });
                    ctx.SaveChanges();
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}