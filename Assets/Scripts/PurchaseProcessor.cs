using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.AccessControl;
using System.Runtime.Serialization;

public class PurchaseProcessor : BaseController
{
    private readonly IShop _shop;
    private readonly ProfilePlayer _model;
    private readonly List<ShopProduct> _products;

    public PurchaseProcessor(IShop shop, ProfilePlayer model, List<ShopProduct> products)
    {
        _shop = shop;
        _model = model;
        _products = products;

        shop.OnSuccessPurchase.Subscribe(OnSuccessPurchase);
    }

   private void OnSuccessPurchase(string productID)
   {
        var product = _products.Find(p => p.Id == productID);
        if(product == null)
        {
            Debug.LogError("Unknown product");
        }

        ApplyProductModification(product.ResourceModification);
   }


    private void ApplyProductModification(ResourceModification productResourceModification)
    {
        switch (productResourceModification.ResourceType)
        {
            case ResourceType.None:
                break;
            case ResourceType.Gold:
                _model.Gold.Value += productResourceModification.Count;
                break;
            default:
                throw new ArgumentOutOfRangeExeption();


        }


    }   

}
    
