using AutoMapper;
using AddBasket;
using AddBasket.Business.Services;
using AddBasket.Entities;
using AddBasket.Helpers;
using AddBasket.Models.DTOs;
using AddBasket.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AddItemTest
{
    public class UnitTest
    {

        private BasketService basketService;

        private Mock<IBasketRepository> basketRepoMock { get; set; }
        
        private Mock<IItemRepository> itemRepoMock { get; set; }

        private Mock<IUserRepository> userRepoMock { get; set; }

        private readonly ApiContext context;


        public UnitTest() 
        {

            userRepoMock = new Mock<IUserRepository>();
            itemRepoMock = new Mock<IItemRepository>();
            basketRepoMock = new Mock<IBasketRepository>();

            var options = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase("TestDb").ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
            context = new ApiContext(options);
          
            basketService = new BasketService(basketRepoMock.Object,userRepoMock.Object,itemRepoMock.Object, context);
       
            List<BasketDTO> basketDTOList = new List<BasketDTO>();

            basketRepoMock.Setup(x => x.CreateorUpdateBasket(It.IsAny<AddedItemDTO>()))
             .Callback((AddedItemDTO addedItem) => {
                 BasketDTO basketDTO = new BasketDTO();
                 basketDTO.ItemId = addedItem.Item.Id;
                 basketDTOList.Add(basketDTO);
             }
             );

            basketRepoMock.Setup(x => x.GetUserBasket(It.IsAny<int>())).Returns(basketDTOList);
            itemRepoMock.Setup(x => x.StockCheck(It.IsAny<AddedItemDTO>())).Returns(true);
            userRepoMock.Setup(x => x.UserExist(It.IsAny<int>())).Returns(true);
            itemRepoMock.Setup(x => x.UpdateCompanyStock(It.IsAny<AddedItemDTO>()));

        }


        [Fact]
        public void Add_Item_to_Basket_Test()
        {

            AddedItemDTO addedItem = new AddedItemDTO();
            var item = new ItemDTO();
            addedItem.Item = item;
            addedItem.Item.Id = 5;
            List<BasketDTO> addedBasketDTOList = basketService.AddItem(addedItem);

         
            Assert.Equal(1, addedBasketDTOList.Count);
            Assert.Equal(addedBasketDTOList.First().ItemId, addedItem.Item.Id);
        }

        [Fact]
        public void GetById_KnownUser_ReturnsTrue()
        {
            // Act
           var result = userRepoMock.Object.UserExist(1);
            // Assert
            Assert.True(result);
        }
        [Fact]
        public void GetById_NotKnownUser_Throws_Exception()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
          .UseInMemoryDatabase("user_not_known_test_db")
          .Options;

            using (var context = new ApiContext(options))
            {
                var repository = new UserRepository(context);              

                Assert.Throws<AppException>(() => repository.UserExist(0));
            }
        }
        [Fact]
        public void Stock_Check_Stock_Available_ReturnsTrue()
        {
            var company = new CompanyDTO();
            var item = new ItemDTO();
            var addedItem = new AddedItemDTO
            {
                Company = company,
                Item = item,
            };
            addedItem.Company.Id = 1;
            addedItem.Item.Id = 1;

            var options = new DbContextOptionsBuilder<ApiContext>()
          .UseInMemoryDatabase("stock_test_db")
          .Options;

            using (var context = new ApiContext(options))
            {
                context.LoadTestDBData(2);
                var repository = new ItemRepository(context);

                Assert.True(repository.StockCheck(addedItem));
            }
        }
        [Fact]
        public void Stock_Check_Stock_NOT_Available_ThrowsException()
        {
            var company = new CompanyDTO();
            var item = new ItemDTO();
            var addedItem = new AddedItemDTO
            {
                Company = company,
                Item = item,
            };
            addedItem.Company.Id = 5;
            addedItem.Item.Id = 5;

            var options = new DbContextOptionsBuilder<ApiContext>()
          .UseInMemoryDatabase("no_stock_test_db")
          .Options;

            using (var context = new ApiContext(options))
            {
                context.LoadTestDBData(2);
                var repository = new ItemRepository(context);

                Assert.Throws<AppException>(() => repository.StockCheck(addedItem));
            }
        }    
        [Fact]
        public void Update_Company_Stock_Returns_BasketDto()
        {

            var company = new CompanyDTO();
            var item = new ItemDTO();
            var addedItem = new AddedItemDTO
            {
                Company = company,
                Item = item,
            };
            addedItem.Company.Id = 1;
            addedItem.Item.Id = 1;

            var basketDtoList = new List<BasketDTO>();
            var basketDto = new BasketDTO
            {
                Id = 1,
                ItemId = 1,
                ItemCount = 1
            };
            basketDtoList.Add(basketDto);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BasketDTO, Basket>().ReverseMap();
            });
            var mapper = mockMapper.CreateMapper();


            var options = new DbContextOptionsBuilder<ApiContext>()
          .UseInMemoryDatabase("company_stock_update_test_db")
          .Options;

            using (var context = new ApiContext(options))
            {
                context.LoadUserBasketTestData(1);
                var companyStock = context.CompanyItem.First(s => s.CompanyId == addedItem.Company.Id && s.ItemId == addedItem.Item.Id).Stock;
               
                var repository = new ItemRepository(context);
                repository.UpdateCompanyStock(addedItem);
               
                var companyStockUpdated = context.CompanyItem.First(s => s.CompanyId == addedItem.Company.Id && s.ItemId == addedItem.Item.Id).Stock;
                Assert.Equal(companyStock-1, companyStockUpdated);
            }
        }
        [Fact]
        public void Get_User_Basket_Returns_BasketDto()
        {
            var basketDtoList = new List<BasketDTO>();
            var basketDto = new BasketDTO
            {
                Id = 1,
                ItemId = 1,
                ItemCount = 1
            };
            basketDtoList.Add(basketDto);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BasketDTO, Basket>().ReverseMap();
            });
            var mapper = mockMapper.CreateMapper();


            var options = new DbContextOptionsBuilder<ApiContext>()
          .UseInMemoryDatabase("user_basket_test_db")
          .Options;

            using (var context = new ApiContext(options))
            {
                context.LoadUserBasketTestData(1);
                var repository = new BasketRepository(context, mapper);
               var tst = basketDtoList.First();
                var act = repository.GetUserBasket(1).First();
                
                Assert.True(tst.ItemId==act.ItemId && tst.ItemCount==act.ItemCount&& tst.Id==act.Id);
            }
        }
    }
}
