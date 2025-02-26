﻿using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
    internal class ShopTools : IShop, IStoreListener
    {
        private IStoreController _controller;
        private IExtensionProvider _extensionProvider;
        private bool _isInitialized;

        private readonly SubscriptionActionT<string> _onSuccessPurchase;

        private readonly SubscriptionAction _onFailedPurchase;

        
        public ShopTools(List<ShopProduct> products)
        {
            _onSuccessPurchase = new SubscriptionActionT<string>();
            _onFailedPurchase = new SubscriptionAction();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (ShopProduct product in products)
            {
                builder.AddProduct(product.Id, product.CurrentProductType);
            }
            UnityPurchasing.Initialize(this, builder);
        }

    public IReadOnlySubscriptionActionT<string> OnSuccessPurchase => _onSuccessPurchase;
    //public event Action<PurchaseInfo> OnSuccessPurchase;
    public IReadOnlySubscriptionAction OnFailedPurchase => _onFailedPurchase;

        public void Buy(string id)
        {
            if (!_isInitialized)
                return;
            _controller.InitiatePurchase(id);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _isInitialized = false;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
        bool validPurchase = false;
#if UNITY_IOS || UNITY_ANDROID

        CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
            try
            {
            IPurchaseReceipt[] result = validator.Validate(purchaseEvent.purchasedProduct.receipt);
            validPurchase = true;
                foreach (IPurchaseReceipt productReceipt in result)
                {
                    validPurchase &= productReceipt.purchaseDate == DateTime.UtcNow;
                }
            }
            catch (IAPSecurityException)
            {
                Debug.Log("Invalid receipt, not unlocking content");
                validPurchase = false;
            }
#endif
        if (validPurchase)
            _onSuccessPurchase.Invoke(purchaseEvent.purchasedProduct.definition.id);
            //_onFailedPurchase?.Invoke();
                return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _onFailedPurchase.Invoke();
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensionProvider = extensions;
            _isInitialized = true;
        }

        public string GetCost(string productID)
        {
            Product product = _controller.products.WithID(productID);

            if (product != null)
                return product.metadata.localizedPriceString;

            return "N/A";
        }

        public void RestorePurchase()
        {
            if (!_isInitialized)
            {
                return;
            }

#if UNITY_IOS
            
           _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoreTransactionFinished);
#else
        _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnRestoreFinished);
#endif
        }

        private void OnRestoreFinished(bool isSuccess)
        {

        }
    }
