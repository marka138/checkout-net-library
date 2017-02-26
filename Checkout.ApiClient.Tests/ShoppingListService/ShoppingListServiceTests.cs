using System.Net;
using Checkout.ApiServices.ShoppingLists.RequestModels;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture(Category = "ShoppingListApi")]
    public class ShoppingListService : BaseServiceTests
    {
        [Test]
        public void CreateShoppingListItemWithCard()
        {
            var shoppingListItemCreateModel = TestHelper.GetShoppingListItemCreateModel();
            var response = CheckoutClient.ShoppingListService.CreateShoppingListItem(shoppingListItemCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            shoppingListItemCreateModel.ShouldBeEquivalentTo(response.Model);
        }

        [Test]
        public void DeleteShoppingListItem()
        {
            var shoppingListItem =
                CheckoutClient.ShoppingListService.CreateShoppingListItem(TestHelper.GetShoppingListItemCreateModel()).Model;

            var response = CheckoutClient.ShoppingListService.DeleteShoppingListItem(shoppingListItem.ItemName);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void GetShoppingListItem()
        {
            var shoppingListItemCreateModel = TestHelper.GetShoppingListItemCreateModel();
            var shoppingListItem = CheckoutClient.ShoppingListService.CreateShoppingListItem(shoppingListItemCreateModel).Model;

            var response = CheckoutClient.ShoppingListService.GetShoppingListItem(shoppingListItem.ItemName);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.ItemName.Should().Be(shoppingListItem.ItemName);
            shoppingListItem.ShouldBeEquivalentTo(response.Model);
        }

        [Test]
        public void GetShoppingListItemList()
        {
            var shoppingListItem1 = CheckoutClient.ShoppingListService.CreateShoppingListItem(TestHelper.GetShoppingListItemCreateModel());
            var shoppingListItem2 = CheckoutClient.ShoppingListService.CreateShoppingListItem(TestHelper.GetShoppingListItemCreateModel());
            var shoppingListItem3 = CheckoutClient.ShoppingListService.CreateShoppingListItem(TestHelper.GetShoppingListItemCreateModel());
            var shoppingListItem4 = CheckoutClient.ShoppingListService.CreateShoppingListItem(TestHelper.GetShoppingListItemCreateModel());

            var custGetListRequest = new ShoppingListItemGetList
            {
                //// if paging required
                //PageNumber = "1",
                //PageSize = 10
            };

            //Get all shoppingListItems created
            var response = CheckoutClient.ShoppingListService.GetShoppingList(custGetListRequest);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Count.Should().BeGreaterOrEqualTo(4);

            response.Model.Data[0].ItemName.Should().Be(shoppingListItem1.Model.ItemName);
            response.Model.Data[1].ItemName.Should().Be(shoppingListItem2.Model.ItemName);
            response.Model.Data[2].ItemName.Should().Be(shoppingListItem3.Model.ItemName);
            response.Model.Data[3].ItemName.Should().Be(shoppingListItem4.Model.ItemName);
        }

        [Test]
        public void UpdateShoppingListItem()
        {
            var shoppingListItem =
                CheckoutClient.ShoppingListService.CreateShoppingListItem(TestHelper.GetShoppingListItemCreateModel()).Model;

            var shoppingListItemUpdateModel = TestHelper.GetShoppingListItemUpdateModel();
            var response = CheckoutClient.ShoppingListService.UpdateShoppingListItem(shoppingListItemUpdateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }
    }
}