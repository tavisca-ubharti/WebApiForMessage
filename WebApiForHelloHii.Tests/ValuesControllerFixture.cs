using WebApiForHelloHii.Controllers;
using Xunit;

namespace WebApiForHelloHii.Tests
{
    public class ValuesControllerFixture
    {
        private ValuesController _valueController;
        public ValuesControllerFixture()
        {
            _valueController = new ValuesController();
        }

        [Fact]
        public void Send_hii_should_return_hello()
        {
            var resultFromCall = _valueController.Get("hii");
            Assert.Equal("hello", resultFromCall);
        }

        [Fact]
        public void Send_hello_should_return_hii()
        {
            var resultFromCall = _valueController.Get("hello");
            Assert.Equal("hii", resultFromCall);
        }
    }
}
