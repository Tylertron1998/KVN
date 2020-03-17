using KVN.Providers;
using KVN.Tests.Stubs;
using NUnit.Framework;

namespace KVN.Tests
{
    public class Tests
    {

        [Test]
        public void Test_Insert()
        {
            // arrange
            
            const string testString = "Hello, world!";
            
            var settings = new DatabaseSettings("./foobar.json");
            var instance = new DatabaseInstance<DatabaseStub>(settings, new JsonProvider<DatabaseStub>());
            var stub = new DatabaseStub { Value = testString };
            
            // act
            instance[1] = stub;
            instance.SaveChanges();
            
            var secondInstance = new DatabaseInstance<DatabaseStub>(settings, new JsonProvider<DatabaseStub>());
            var value = secondInstance[1].Value;
            
            // assert
            Assert.AreEqual(testString, value);

        }
    }
}