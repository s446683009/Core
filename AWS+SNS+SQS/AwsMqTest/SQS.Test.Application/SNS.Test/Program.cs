using System;
using Amazon.Runtime.CredentialManagement;
using Amazon.SimpleNotificationService;
using System.Threading.Tasks;
using System.Linq;
using Amazon.SimpleNotificationService.Model;
using System.Collections.Generic;
using Amazon.SQS;
namespace SNS.Test
{
    class Program
    {
        /// <summary>
        /// 创建sns 主题 
        /// sqs 订阅该主题
        /// 发布消息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            string topicArn;
            var sharedFile = new SharedCredentialsFile();
            sharedFile.TryGetProfile("default", out var sourceProfile);

            var credentials = AWSCredentialsFactory.GetAWSCredentials(sourceProfile, sharedFile);

            AmazonSimpleNotificationServiceClient client = new AmazonSimpleNotificationServiceClient(credentials,new AmazonSimpleNotificationServiceConfig() { 
                    ServiceURL="http://192.168.1.57:4566"
            });

            AmazonSQSClient as_client = new AmazonSQSClient(credentials,new AmazonSQSConfig() { 
                    ServiceURL= "http://192.168.1.57:4566"
            });
            //查找主题
            var l_response = await client.ListTopicsAsync();
            if (!l_response.Topics.Any())
            {
                //没有主题就创建主题
                var c_response = await client.CreateTopicAsync("test");
                topicArn = c_response.TopicArn;
                Console.WriteLine("topic create success,topicArn is {0}", c_response.TopicArn);
            }
            else {

                topicArn = l_response.Topics.First().TopicArn;
            }
            //Subscription subscriptions;
            //var ls_response = await client.ListSubscriptionsByTopicAsync(topicArn);
            //if (!ls_response.Subscriptions.Any(t => t.SubscriptionArn == "arn:aws:sns:us-east-1:000000000000:test:bac7453b-ae89-4021-81f2-044890c0d68c")) {
            //    //如果没有订阅创建订阅
            // var s_response= await client.SubscribeQueueToTopicsAsync(new List<string>() { topicArn},as_client, "http://localhost:4566/000000000000/test");
              

            //}
            var p_response=await client.PublishAsync(topicArn,"from sns publish message");

            Console.WriteLine("success");

            Console.ReadLine();

        }
    }
}
