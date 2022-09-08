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
using FluentValidation.Results;
using FluentAssertions;

namespace UnitTest
{
    [TestFixture]
    public class Tests
    {

        PokemonController controller = null;
        iDbRepo repo = null;
        IHttpClientFactory client = null;
        IWebAPIDBContext dbContextMock = null;

        myValidator validator = new myValidator();
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
            
            deletedMove.Should().NotBeNull();


            IActionResult notDeleted = con.deleteMove("belly-drum");
            notDeleted.Should().NotBeNull();
            deleted.Should().BeTrue();
        }

        [Test]
        public async Task Test_Repository_and_DBContext()
        {
            var data = repo.GetMoveByName("mega-punch");
            ValidationResult validateResult3 = validator.Validate(data);
            validateResult3.IsValid.Should().BeTrue();

            data.move.Should().Be("mega-punch");

            Move slam = new Move { move = "ice-punch", name = "froslass" };
            var data2 = repo.updateMoves(slam);
            ValidationResult validateResult4 = validator.Validate(data2);
            validateResult4.IsValid.Should().BeTrue();
            data2.move.Should().Be("ice-punch");

        }

        [Test]
        public async Task getMove()
        {

            var repos = Substitute.For<iDbRepo>();

         
            repos.GetMoveByName("mega-punch").Returns(new Move { move = "mega-punch", name = "snorlax" });
            var con = new PokemonController(client, repos);

            IActionResult gottenMove = con.GetMovesByName("mega-punch");
            gottenMove.Should().NotBeNull();

            var result1 = ((Move)((ObjectResult)gottenMove).Value);
            ValidationResult validateResult = validator.Validate(result1);
            
            validateResult.IsValid.Should().BeTrue();

            IActionResult notAMove = con.GetMovesByName("belly-drum");
            notAMove.Should().NotBeNull();

            var result2 = ((string)((ObjectResult)notAMove).Value);
            
            result1.move.Should().Be("mega-punch");
            result2.Should().Be("No pokemon with the move belly-drum");



        }

        [Test]
        public async Task updateMoves()
        {
            Boolean moveChanged = false;
            var repository = Substitute.For<iDbRepo>();
            repository.GetMoveByName("mega-punch")
                .Returns(new Move { move = "mega-punch", name = "snorlax"});
            repository.When(x => x.updateMoves(Arg.Any<Move>()))
                .Do(x => moveChanged = true);
            var c = new PokemonController(client, repository);

            IActionResult updateMove = c.updateMoves("mega-punch", "clefable");
            updateMove.Should().NotBeNull();
            var result1 = ((Move)((ObjectResult)updateMove).Value);
            
            moveChanged.Should().BeTrue();

            moveChanged = false;
            IActionResult updateMove2 = c.updateMoves("hurricane", "groudon");
            
            updateMove2.Should().NotBeNull();
            Assert.IsNotInstanceOf<Move>(updateMove2);
            moveChanged.Should().BeFalse();
        }
    }
}