using System.Collections.Generic;

namespace Checkout.ApiServices.ShoppingLists.ResponseModels
{
    public class ShoppingList
    {
        public int Count{ get; set; }
        public List<ShoppingListItem> Data { get; set; }
    }
}