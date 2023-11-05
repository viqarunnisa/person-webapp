using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonWebApp.Controllers;
using PersonWebApp.Models;
using PersonWebApp.Services;

namespace PersonWebAppTests
{
    public class PersonControllerTest
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfPerson()
        {
            // Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(s => s.GetPeople())
                .Returns(GetTestPeople());
            var controller = new PersonController(mockService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Person>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Add_ReturnsAViewResultWithError_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IPersonService>();
            var controller = new PersonController(mockService.Object);
            controller.ModelState.AddModelError("FirstName", "Required");
            var newPerson = GetTestPerson();
            
            // Act
            var result = controller.Add(newPerson);
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(1, viewResult.ViewData.ModelState.ErrorCount);
        }

        [Fact]
        public void Add_AddsPersonAndReturnsARedirect_WhenModelStateIsValid()
        {
            // Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(s => s.AddPerson(It.IsAny<Person>())).Verifiable();
            var controller = new PersonController(mockService.Object);
            var newPerson = GetTestPerson();

            // Act
            var result = controller.Add(newPerson);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockService.Verify();
        }

        private IEnumerable<Person> GetTestPeople()
        {
            return new List<Person>()
            {
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Richard",
                    LastName = "Roe"
                }
            };
        }

        private Person GetTestPerson()
        {
            return new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            };
        }
    }
}