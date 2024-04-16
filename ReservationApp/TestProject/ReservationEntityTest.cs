using ReservationApp.Models;
using System.ComponentModel.DataAnnotations;

namespace TestProject
{
    public class ValidationResult
    {
        public bool IsValid { get; }

        public ValidationResult(System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult> validationResults)
        {
            IsValid = validationResults.Count == 0;
        }
    }
    public class ReservationEntityTest
    {
        private static ValidationResult ValidateModel(object model)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            var validationResults = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return new ValidationResult(validationResults);
        }

        #region Valid data
        [Fact]
        public void Reservation_ValidData_ShouldBeValid()
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = "TestAddress",
                NumberOfPeople = 2,
                Owner = "TestOwner",
                NumberOfNights = 3
            };

            var validationResult = ValidateModel(reservation);
            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region Missing data        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Reservation_MissingCity_ShouldBeInvalid(string city)
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = city,
                Address = "TestAddress",
                NumberOfPeople = 2,
                Owner = "TestOwner",
                NumberOfNights = 3
            };

            var validationResult = ValidateModel(reservation);
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Reservation_MissingAddress_ShouldBeInvalid(string address)
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = address,
                NumberOfPeople = 2,
                Owner = "TestOwner",
                NumberOfNights = 3
            };

            var validationResult = ValidateModel(reservation);
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(null)]
        public void Reservation_MissingNumberOfPeople_ShouldBeInvalid(int numberOfPeople)
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = "TestAddress",
                NumberOfPeople = numberOfPeople,
                Owner = "TestOwner",
                NumberOfNights = 3
            };

            var validationResult = ValidateModel(reservation);
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Reservation_MissingOwner_ShouldBeInvalid(string owner)
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = "TestAddress",
                NumberOfPeople = 1,
                Owner = owner,
                NumberOfNights = 3
            };

            var validationResult = ValidateModel(reservation);
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [InlineData(null)]
        public void Reservation_MissingNumberOfNights_ShouldBeInvalid(int numberOfNights)
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = "TestAddress",
                NumberOfPeople = 1,
                Owner = "TestOwner",
                NumberOfNights = numberOfNights
            };

            var validationResult = ValidateModel(reservation);
            Assert.False(validationResult.IsValid);
        }
        #endregion

        #region Invalid data
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(11)]
        public void Reservation_InvalidNumberOfPeople_ShouldBeInvalid(int numberOfPeople)
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = "TestAddress",
                RoomId = 1,
                NumberOfPeople = numberOfPeople,
                Owner = "TestOwner",
                NumberOfNights = 3
            };

            var validationResult = ValidateModel(reservation);

            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public void Reservation_NegativeNumberOfNights_ShouldBeInvalid()
        {
            var reservation = new Reservation
            {
                Date = DateTime.Now.AddDays(1),
                City = "TestCity",
                Address = "TestAddress",
                RoomId = 1,
                NumberOfPeople = 2,
                Owner = "TestOwner",
                NumberOfNights = -3
            };

            var validationResult = ValidateModel(reservation);

            Assert.False(validationResult.IsValid);
        }
        #endregion



    }


}
