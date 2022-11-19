using Microsoft.AspNetCore.Identity;
using Neo4j.Driver;
using WeBScraper_CourseProject_.Data;

namespace WeBScraper_CourseProject_
{
    public class Neo4
    {
        private readonly List<Graphs> _graps;
        private readonly UserManager<ApplicationUser> _userManager;

        public Neo4(List<Graphs> graps, UserManager<ApplicationUser> userManager)
        {
            _graps = graps;
            _userManager = userManager;
        }


        [Obsolete]
        public async void Execute()
        {
            using var greeter = new HelloWorldExample("bolt://localhost:7687", "neo4j", "123456");

            var unique = _graps.Where(w => w.UserName.Length > 0).DistinctBy(x => x.UserName);

            await greeter.Delete();
            

            for (int i = 0; i < _graps.Count; i++)
            {
                greeter.News(_graps[i].NTitle);

                if (i == 0)
                {
                    foreach (var item in unique)
                    {
                        greeter.User(item.UserName);
                    }
                }                
                    
                //if (i > 0)
                //{                    

                //if (_graps[i].UserName != _graps[i - 1].UserName)
                //{
                //    greeter.Person(_graps[i].UserName);
                //}
                //}
                greeter.UserNewsRel(_graps[i].UserName, _graps[i].NTitle);

            }

        }
    }   

    public class HelloWorldExample : IDisposable
    {
        private bool _disposed;
        private readonly IDriver _driver;

        ~HelloWorldExample() => Dispose(false);

        public HelloWorldExample(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        [Obsolete]
        public void News(string title)
        {
            using var session = _driver.Session();

            var greeting = session.WriteTransaction(tx =>
            {
                return tx.Run("CREATE (a:News {title: $title})",
                    new { title }).Consume();
            });
        }

        [Obsolete]
        public void User(string username)
        {
            using var session = _driver.Session();


            var greeting = session.WriteTransaction(tx =>
            {
                return tx.Run("CREATE (a:User {username: $username})",
                    new { username }).Consume();
            });
        }

        [Obsolete]
        public void UserNewsRel(string username, string title)
        {
            using var session = _driver.Session();


            var greeting = session.WriteTransaction(tx =>
            {
                return tx.Run(@"MATCH (user:User {username: $username}) 
                                    MATCH (news: News {title: $title}) 
                                    MERGE (user)-[:LIKES]->(news)", new { username, title }).Consume();
            });
        }

        [Obsolete]
        public async Task Delete()
        {
            using var session = _driver.Session();


            var greeting = session.WriteTransaction(tx =>
            {
                return tx.Run("MATCH (n) OPTIONAL MATCH (n)-[r]-() DELETE n, r", new {}).Consume();
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _driver?.Dispose();
            }

            _disposed = true;
        }

    }

}