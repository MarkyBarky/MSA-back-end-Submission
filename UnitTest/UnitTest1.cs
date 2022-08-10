using NUnit.Framework;
using MSA.backend.Api;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MSA.backend.Data;
using MSA.backend.Api.Model;
using MSA.backend.Api.Controllers;
using System.Net.Http;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest
{
    [TestFixture]
    public class Tests
    {

        PokemonController controller = null;
        iDbRepo repo = null;
        IHttpClientFactory client = null;
        IWebAPIDBContext dbContextMock = null;

        [SetUp]
        public void Setup()
        {
            var mockPokemons = new List<Move>()
            {
                new Move {move = "mega-punch", name = "snorlax"},
                new Move {move = "slam", name = "azurill"},
                new Move {move = "pay-day", name = "meowth"},
                new Move {move = "ice-punch", name = "froslass"},

            }.AsQueryable();

            var mockSet = Substitute.For<DbSet<Move>, IQueryable<Move>>();

            // setup a mock dbset Pokemons
            ((IQueryable<Move>)mockSet).Provider.Returns(mockPokemons.Provider);
            ((IQueryable<Move>)mockSet).Expression.Returns(mockPokemons.Expression);
            ((IQueryable<Move>)mockSet).ElementType.Returns(mockPokemons.ElementType);
            ((IQueryable<Move>)mockSet).GetEnumerator().Returns(mockPokemons.GetEnumerator());
            dbContextMock = Substitute.For<IWebAPIDBContext>();
            dbContextMock.moves.Returns(mockSet);

            repo = Substitute.For<DbRepo>(dbContextMock);
            client = Substitute.For<IHttpClientFactory>();
            controller = new PokemonController(client, repo);

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task Delete()
        {
            var deleted = false;
            
            var repos = Substitute.For<iDbRepo>();
            
            repos.When(x => x.deleteMoves(Arg.Any<Move>()))
                .Do(x => deleted=true);
            repos.GetMoveByName("mega-punch")
                .Returns(new Move { move = "mega-punch", name = "snorlax"});
            var con = new PokemonController(client, repos);

            IActionResult deletedMove = con.deleteMove("mega-punch");
            Assert.IsNotNull(deletedMove);

            IActionResult notDeleted = con.deleteMove("belly-drum");
            Assert.IsNotNull(notDeleted);

            Assert.AreEqual(true, deleted);
        }

        [Test]
        public async Task Test_Repository_and_DBContext()
        {
            var data = repo.GetMoveByName("mega-punch");
            Assert.AreEqual("mega-punch", data.move);
        }

        [Test]
        public async Task getMove()
        {

            

            var repos = Substitute.For<iDbRepo>();

         
            repos.GetMoveByName("mega-punch").Returns(new Move { move = "mega-punch", name = "snorlax" });
            var con = new PokemonController(client, repos);

            IActionResult gottenMove = con.GetMovesByName("mega-punch");
            Assert.IsNotNull(gottenMove);

            var result1 = ((Move)((ObjectResult)gottenMove).Value);

            IActionResult notAMove = con.GetMovesByName("belly-drum");
            Assert.IsNotNull(notAMove);

            var result2 = ((string)((ObjectResult)notAMove).Value);

            Assert.AreEqual("mega-punch", result1.move);
            Assert.AreEqual("No pokemon with the move belly-drum", result2);

        }

    }
}