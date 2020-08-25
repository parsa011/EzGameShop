using System;
using System.Collections.Generic;
using System.Linq;
using EzGame.Data.Interfaces;
using EzGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EzGame.Data.Test.RepositoriesTest
{
    [TestClass]
    public class ReposTest
    {
        [TestMethod]
        public void GetAll_Platforms_Test()
        {
            var list = new List<Platform>
            {
                new Platform
                {
                    CreatedTime = DateTime.Now,
                    Logo = null,
                    Title = "C#",
                    LastModifiedTime = DateTime.Now
                },
                new Platform
                {
                    CreatedTime = DateTime.Now,
                    Logo = null,
                    Title = "Asp.net core",
                    LastModifiedTime = DateTime.Now
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Platform>>();
            mockSet.As<IQueryable<Platform>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Platform>>().Setup(m => m.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<Platform>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Platform>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator);
            
            var mockContext = new Mock<IUnitOfWork>();
            mockContext.Setup(c => c.PlatformRepository.GetAll()).Returns(mockSet.Object);

            //Act
            var actual = mockContext.Object.PlatformRepository.GetAll();
            var platforms = actual.ToList();
            Assert.AreEqual(2, platforms.Count());
            Assert.AreEqual("C#", platforms.First().Title);
        }
    }
}