using MinimalWebAPI.Models;

namespace MinimalAPI.Test
{
    [TestClass]
    public class TestEndpoints
    {
        private readonly TodoDb _db;

        public TestEndpoints(TodoDb db)
        {
            _db = db;
        }

        [TestMethod]
        public async Task GetAllTodosAsync_ReturnsOkOfTodosResult()
        {
            await Task.CompletedTask;
        }
    }

}