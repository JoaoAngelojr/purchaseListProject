using FluentValidation.Results;
using PurchaseList.API.Requests;
using PurchaseList.API.Validators.PurchaseLists;
using PurchaseList.API.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace PurchaseList.Tests.ValidatorsTests
{
    public class PurchaseListRequestValidatorTest
    {
        private readonly PurchaseListRequestValidator _validator;
        private readonly CalculateBillsRequest _defaultRequest;
        private readonly List<string> emptyStringList;

        public PurchaseListRequestValidatorTest()
        {
            _validator = new PurchaseListRequestValidator();
            emptyStringList = new List<string>();

            List<ItemViewModel> items = new List<ItemViewModel>();
            ItemViewModel item1 = new ItemViewModel()
            {
                Quantity = 1,
                Price = 5
            };
            items.Add(item1);

            ItemViewModel item2 = new ItemViewModel()
            {
                Quantity = 2,
                Price = 3
            };
            items.Add(item2);

            List<string> emails = new List<string>();
            emails.Add("email1@email.com");
            emails.Add("email2@email.com");

            _defaultRequest = new CalculateBillsRequest()
            {
                Items = items,
                Emails = emails
            };
        }

        [Fact]
        public void WhenSendValidRequestThenShouldBeSuccess()
        {
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void WhenEmailsListIsNullThenShouldBeFail()
        {
            _defaultRequest.Emails = null;
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("You need to add at least one email.", result.ToString());
        }

        [Fact]
        public void WhenEmailsListIsEmptyThenShouldBeFail()
        {
            _defaultRequest.Emails = new List<string>();
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("You need to add at least one email.", result.ToString());
        }

        [Fact]
        public void WhenItemsListIsNullThenShouldBeFail()
        {
            _defaultRequest.Items = null;
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("You need to add at least one item.", result.ToString());
        }

        [Fact]
        public void WhenItemsListIsEmptyThenShouldBeFail()
        {
            _defaultRequest.Items = new List<ItemViewModel>();
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("You need to add at least one item.", result.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenEmailsListHasANullOrEmptyEmailThenShouldBeFail(string email)
        {
            _defaultRequest.Emails[0] = email;
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("None of emails cannot be null or empty", result.ToString());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void WhenItemsListHasAnItemWithItsQuantityAsNegativeNumberThenShouldBeFail(int quantity)
        {
            _defaultRequest.Items[0].Quantity = quantity;
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("The item quantity cannot be a negative number.", result.ToString());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void WhenItemsListHasAnItemWithItsPriceAsNegativeNumberThenShouldBeFail(int price)
        {
            _defaultRequest.Items[0].Price = price;
            ValidationResult result = _validator.Validate(_defaultRequest);
            Assert.False(result.IsValid);
            Assert.Equal("The item price cannot be a negative number.", result.ToString());
        }
    }
}