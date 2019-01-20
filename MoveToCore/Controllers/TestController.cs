using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MoveToCore.Config;
using MoveToCore.Models;
using MoveToCore.SignalR;
using MoveToCore.ViewModels;

namespace MoveToCore.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        public MongoCollection<Test> TestCollection { get; set; }
        public IHubContext<DistributionHub> DistributionHubContext { get; set; }

        public TestController(IHubContext<DistributionHub> distributionHubContext)
        {
            DistributionHubContext = distributionHubContext;
            TestCollection = DB.Instance.Db.GetCollection<Test>(nameof(Test).ToLower());
        }

        [HttpGet]
        public OkObjectResult GetAll()
        {
            return Ok(new
            {
                all = TestCollection.FindAll().Select(x => new TestViewModel(x))
            });
        }

        [HttpPost]
        public async Task<OkResult> AddNew([FromBody]TestViewModel testViewModel)
        {
            var test = testViewModel.ToModel();

            TestCollection.Insert(test);

            await DistributionHubContext.Clients.All.SendCoreAsync("distribution", new object[]
            {
                new DistributionData
                {
                    Type = DistributionType.Insert,
                    Item = test
                }
            });

            return Ok();
        }

        [HttpPut]
        public async Task<OkResult> Update(TestViewModel test)
        {
            var targetTest = TestCollection.FindOne(Query<Test>.EQ(x => x.Message, test.Message));

            targetTest.Comments = test.Comments;

            TestCollection.Save(targetTest);

            await DistributionHubContext.Clients.All.SendCoreAsync("distribution", new object[]
            {
                new DistributionData
                {
                    Type = DistributionType.Update,
                    Item = targetTest
                }
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(string id)
        {
            var testId = ObjectId.Parse(id);

            TestCollection.Remove(Query<Test>.EQ(x => x.Id, testId));

            await DistributionHubContext.Clients.All.SendCoreAsync("distribution", new object[]
            {
                new DistributionData
                {
                    Type = DistributionType.Delete,
                    Item = new Test {Id = testId}
                }
            });

            return Ok();
        }

        [HttpGet("Exception")]
        public void Exception()
        {
            var exceptions = new List<Exception>
            {
                new Exception("Oh noo!!!", new NullReferenceException()),
                new ArgumentException("The argument Wow dose not exist ;("),
                new NullReferenceException(),
            };

            throw exceptions[new Random().Next(exceptions.Count)];
        }
    }
}