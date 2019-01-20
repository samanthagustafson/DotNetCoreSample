using MongoDB.Bson;
using MoveToCore.Models;

namespace MoveToCore.ViewModels
{
    public class TestViewModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Comments { get; set; }

        public TestViewModel()
        {
            
        }

        public TestViewModel(Test test)
        {
            Id = test.Id.ToString();
            Message = test.Message;
            Comments = test.Comments;
        }

        public Test ToModel()
        {
            return new Test
            {
                Id = !string.IsNullOrEmpty(Id) ? ObjectId.Parse(Id) : ObjectId.Empty,
                Message = Message,
                Comments = Comments
            };
        }
    }
}
