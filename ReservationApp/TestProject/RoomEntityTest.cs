using ReservationApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class RoomEntityTests
    {
        private ValidationResult ValidateModel(object model)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            var validationResults = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return new ValidationResult(validationResults);
        }

        #region Valid data
        [Fact]
        public void Room_ValidData_ShouldBeValid()
        {
            var room = new Room
            {
                RoomNumber = 101,
                Price = 100.00m,
                MaxPeopleNumber = 2
            };

            var validationResult = ValidateModel(room);
            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region Missing data
        [Fact]
        public void Room_MissingMaxPeopleNumber_ShouldBeInvalid()
        {
            var room = new Room
            {
                RoomNumber = 101,
                Price = 100.00m,
                MaxPeopleNumber = 0
            };

            var validationResult = ValidateModel(room);
            Assert.False(validationResult.IsValid);
        }
        #endregion

        #region Invalid data
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Room_InvalidRoomNumber_ShouldBeInvalid(int number)
        {
            var room = new Room
            {
                RoomNumber = number,
                Price = 100.00m,
                MaxPeopleNumber = 2 
            };

            var validationResult = ValidateModel(room);
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Room_InvalidPrice_ShouldBeInvalid(decimal price)
        {
            var room = new Room
            {
                RoomNumber = 101,
                Price = price,
                MaxPeopleNumber = 2
            };

            var validationResult = ValidateModel(room);
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Room_InvalidMaxPeopleNumber_ShouldBeInvalid(int peopleNumber)
        {
            var room = new Room
            {
                RoomNumber = 101,
                Price = 100.00m,
                MaxPeopleNumber = peopleNumber
            };

            var validationResult = ValidateModel(room);
            Assert.False(validationResult.IsValid);
        }
        #endregion
    }
}
