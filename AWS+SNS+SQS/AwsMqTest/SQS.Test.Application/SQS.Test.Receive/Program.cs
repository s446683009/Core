using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using System;
using System.Threading.Tasks;
namespace SQS.Test.Receive
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sharedFile = new SharedCredentialsFile();
            sharedFile.TryGetProfile("default", out var sourceProfile);

            var credentials = AWSCredentialsFactory.GetAWSCredentials(sourceProfile, sharedFile);
            var _client = new AmazonSQSClient(credentials, new AmazonSQSConfig()
            {
                ServiceURL = "http://192.168.1.57:4566"
            });
           
           
            for (int i = 0; i < 10; i++)
            {
                var r_response = await _client.ReceiveMessageAsync("http://localhost:4566/000000000000/test");

                //不在队列中删除信息 无法获取新的消息
                foreach (var item in r_response.Messages)
                {
                    Console.WriteLine(GetTimeFromTimeStamp(long.Parse(item.Attributes["SentTimestamp"])).ToString("yyyy-MM-dd HH:mm") + ":" + item.Body);
                    await _client.DeleteMessageAsync("http://localhost:4566/000000000000/test",item.ReceiptHandle);
                }
                //防止http请求未结束 导致接收重复信息
                System.Threading.Thread.Sleep(200);


            }

            Console.ReadLine();
        }


        static DateTime GetTimeFromTimeStamp(long timestamp,int offset=8) {
            var initDate = new DateTime(1970,1,1,offset,0,0);
            return initDate.AddMilliseconds(timestamp);
        }
    }
}
