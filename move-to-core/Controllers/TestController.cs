using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MoveToCore.Config;
using MoveToCore.Models;

namespace MoveToCore.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        public MongoCollection<Test> TestCollection { get; set; }

        public TestController()
        {
            TestCollection = DB.Instance.Db.GetCollection<Test>(nameof(Test).ToLower());
        }

        [HttpGet]
        public OkObjectResult GetAll()
        {
            var allTests = TestCollection.FindAll().ToList();

            return Ok(allTests);
        }

        [HttpPost]
        public OkResult AddNew(Test test)
        {
            TestCollection.Insert(test);

            return Ok();
        }

        [HttpPut]
        public OkResult Update(Test test)
        {
            var targetTest = TestCollection.FindOne(Query<Test>.EQ(x => x.Message, test.Message));

            targetTest.Comments = test.Comments;

            TestCollection.Save(targetTest);

            return Ok();
        }

        [HttpDelete("{id}")]
        public OkResult Delete(string id)
        {
            TestCollection.Remove(Query<Test>.EQ(x => x.Id, ObjectId.Parse(id)));

            return Ok();
        }

        [HttpGet("Exception")]
        public void Exception()
        {
            var exceptions = new List<Exception>()
            {
                new Exception("Oh noo!!!", new NullReferenceException()),
                new ArgumentException("The argument Wow dose not exist ;("),
                new NullReferenceException(),
            };

            throw exceptions[new Random().Next(exceptions.Count)];
        }
    }
}