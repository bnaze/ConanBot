﻿using System;   
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using TweetSharp;

namespace DSPiggy.Modules
{
    public class TweetView : ModuleBase<SocketCommandContext>
    {
        private static string customer_key;
        private static string customer_key_secret;
        private static string access_token;
        private static string access_token_secret;

        public TweetView()
        {
            //Add own customer and acces token information here for TweetSharp library
            customer_key = "";
            customer_key_secret = "";
            access_token = "";
            access_token_secret = "";
        }

        [Command("tweet")]
        public async Task TweetRandom()
        {
            var service = new TwitterService(customer_key, customer_key_secret);
            service.AuthenticateWith(access_token, access_token_secret);

            var koptions = new ListTweetsOnUserTimelineOptions { ScreenName = "ConanOBrien" };
            List <long> totalID = new List<long>();
            long checker = 2;
            
            foreach (var tweet in service.ListTweetsOnUserTimeline(koptions))
            {
                long statusID = tweet.Id;
                checker = tweet.Id;
                totalID.Add(statusID);
                Console.WriteLine("THIS IS ID: " + statusID);
            }

            Console.WriteLine("THIS IS TOTAL AMOUNT: " + totalID.Count);
            Random roll = new Random();
            int statusToOut = roll.Next(0, totalID.Count);
            Console.WriteLine("STATUS NUMBER TO BE POSTED: " + statusToOut);

            var tweetToOut = totalID[statusToOut];
            Console.WriteLine("STATUS ID TO BE POSTED: " + tweetToOut);

            if(totalID.Count == 1){
                if(tweetToOut == 0){
                    await ReplyAsync("I can't retrieve any tweets, must be the works of... Jordon Schlansky");
                }
            }
            else if(tweetToOut == 0){
                await ReplyAsync("Here's your tweet");
                Random rollAgain = new Random();
                int statusToOutAgain = rollAgain.Next(0, totalID.Count-1);
                Console.WriteLine("STATUS NUMBER TO BE RE-POSTED: " + statusToOutAgain);

                var tweetToOutAgain = totalID[statusToOutAgain];
                Console.WriteLine("STATUS ID TO BE POSTED: " + tweetToOutAgain);
                await ReplyAsync("https://twitter.com/ConanOBrien/status/" + tweetToOutAgain);
            }
            else{
                await ReplyAsync("https://twitter.com/ConanOBrien/status/" + tweetToOut);
            }
        }

        [Command("update")]
        public async Task LatestTweet()
        {
            var service = new TwitterService(customer_key, customer_key_secret);
            service.AuthenticateWith(access_token, access_token_secret);

            var koptions = new ListTweetsOnUserTimelineOptions { ScreenName = "ConanOBrien" };
            string statusID = "";
            foreach (var tweet in service.ListTweetsOnUserTimeline(koptions))
            {
                statusID = tweet.IdStr;
                break;
            }

            Random roller = new Random();
            await ReplyAsync("https://twitter.com/ConanOBrien/status/" + statusID);

        }
    }
}
