using Microsoft.AspNetCore.Mvc;
using Moq;
using PromotionEngine.Controllers;
using PromotionEngine.Models;
using PromotionEngine.PromotionEngine.BusinessLayer;
using System;
using Xunit;

namespace PromotionEngine.Test
{
    public class PromotionControllerTest
    {
        [Fact]
        public void CalculateTotalAmout_ReturnsBadRequestResult_WhenModelBodyIsEmpty()
        {
            Mock<IPromotionEngineBusinessRepo> promotionExecutorMock = new Mock<IPromotionEngineBusinessRepo>();
            PromotionController promotionController = new PromotionController(promotionExecutorMock.Object);
           
            var result = promotionController.CalculateTotalAmout(null);
           
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void CalculateTotalAmout_ReturnsOk_WhenModelBodyIsNotEmpty()
        {
            CartItems[] items=new CartItems[0];
            Mock<IPromotionEngineBusinessRepo> promotionExecutorMock = new Mock<IPromotionEngineBusinessRepo>();
            PromotionController promotionController = new PromotionController(promotionExecutorMock.Object);

            var result = promotionController.CalculateTotalAmout(items);

            Assert.IsType<OkResult>(result);
        }
    }
}
