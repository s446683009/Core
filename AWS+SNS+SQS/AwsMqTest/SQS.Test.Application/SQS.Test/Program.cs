using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Linq;
namespace SQS.Test
{
    /// <summary>
    /// 创建队列
    /// 发送消息
    /// </summary>
    class Program
    {
        static AmazonSQSClient _client;
        static async Task Main(string[] args)
        {

            string url;
            var sharedFile = new SharedCredentialsFile();
            sharedFile.TryGetProfile("default", out var sourceProfile);

            var credentials = AWSCredentialsFactory.GetAWSCredentials(sourceProfile, sharedFile);
            _client = new AmazonSQSClient(credentials, new AmazonSQSConfig()
            {
                ServiceURL = "http://192.168.1.57:4566"
            });

            ListQueuesResponse response = await _client.ListQueuesAsync(new ListQueuesRequest());
            if (!response.QueueUrls.Any())
            {
                CreateQueueResponse c_response = await _client.CreateQueueAsync("test");
                if (c_response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    url = c_response.QueueUrl;
                    Console.WriteLine("create queue success");
                }else
                {
                    Console.WriteLine("create queue fail");
                    return;
                }

            }
            else {
                url = response.QueueUrls.First();
            }

      
            //var msg = Console.ReadLine();
            Console.WriteLine(url);
            //SendMessageResponse s_reponse=await _client.SendMessageAsync(url,msg);
            //Console.WriteLine(response.HttpStatusCode.ToString());
            Console.ReadLine();
        }


    }
}