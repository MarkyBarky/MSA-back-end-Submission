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

       
           
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}