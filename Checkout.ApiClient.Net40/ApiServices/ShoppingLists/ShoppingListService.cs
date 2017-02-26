using Checkout.ApiServices.ShoppingLists.RequestModels;
using Checkout.ApiServices.ShoppingLists.ResponseModels;
using Checkout.ApiServices.SharedModels;
using Checkout.Utilities;

namespace Checkout.ApiServices.ShoppingLists
{
    public class ShoppingListService {
     
    public HttpResponse<ShoppingListItem> CreateShoppingListItem(ShoppingListItemCreate requestModel)
    {
        return new ApiHttpClient().PostRequest<ShoppingListItem>(ApiUrls.ShoppingLists, AppSettings.SecretKey, requestModel);
    }

    public HttpResponse<OkResponse> UpdateShoppingListItem(ShoppingListItemUpdate requestModel)
    {
        return new ApiHttpClient().PutRequest<OkResponse>(ApiUrls.ShoppingLists, AppSettings.SecretKey, requestModel);
    }

    public HttpResponse<OkResponse> DeleteShoppingListItem(string itemName)
    {
        var deleteShoppingListItemUri = string.Format(ApiUrls.ShoppingList, itemName);
        return new ApiHttpClient().DeleteRequest<OkResponse>(deleteShoppingListItemUri, AppSettings.SecretKey);
    }

    public HttpResponse<ShoppingListItem> GetShoppingListItem(string itemName)
    {
        var getShoppingListItemUri = string.Format(ApiUrls.ShoppingList, itemName);
        return new ApiHttpClient().GetRequest<ShoppingListItem>(getShoppingListItemUri, AppSettings.SecretKey);
    }

    public HttpResponse<ShoppingList> GetShoppingList(ShoppingListItemGetList request)
    {
        var getShoppingListUri = ApiUrls.ShoppingLists;

        if (string.IsNullOrWhiteSpace(request.PageNumber))
        {
            getShoppingListUri = UrlHelper.AddParameterToUrl(getShoppingListUri, "PageNumber", request.PageNumber.ToString());
        }

        if (request.PageSize.HasValue)
        {
            getShoppingListUri = UrlHelper.AddParameterToUrl(getShoppingListUri, "PageSize", request.PageSize.ToString());
        }
            
        return new ApiHttpClient().GetRequest<ShoppingList>(getShoppingListUri, AppSettings.SecretKey);
    }
}

}