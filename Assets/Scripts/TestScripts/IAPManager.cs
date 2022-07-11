using System;
using Controllers;
using UnityEngine;
using UnityEngine.Purchasing;
using ScriptableObjects;
using Zenject;

namespace Managers
{
    public class IAPManager : IStoreListener, IInitializable, IDisposable
    {
        private static IExtensionProvider _extensionProvider;
        private static IStoreController _storeController;

        [Inject] private CoinsController _coinsController;
        [Inject] private PurchasePreset purchasePreset;

        public event Action<float> OnPurchaseIsSucceeded;

        public void Initialize()
        {
            if (_storeController == null)
            {
                InitializePurchasing();
            }
            OnPurchaseIsSucceeded += _coinsController.AddCoins;
        }

        public void Dispose()
        {
            OnPurchaseIsSucceeded -= _coinsController.AddCoins;
        }
        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            for (int i = 0; i < purchasePreset.purchases.Count; i++)
            {
                builder.AddProduct(purchasePreset.purchases[i].purchaseName, ProductType.Consumable);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
            return _storeController != null && _extensionProvider != null;
        }

        public void BuyProduct(string productID)
        {
            BuyProductID(productID);
        }

        private void BuyProductID(string productID)
        {
            if (IsInitialized())
            {
                Product product = _storeController.products.WithID(productID);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    _storeController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. " +
                              "Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");

                var apple = _extensionProvider.GetExtension<IAppleExtensions>();

                apple.RestoreTransactions((result) =>
                {
                    Debug.Log("RestorePurchases continuing: " + result + "." +
                              " If no further messages, no purchases available to restore.");
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform." +
                          " Current = " + Application.platform);
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");
            _storeController = controller;
            _extensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason: " + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            for (int i = 0; i < purchasePreset.purchases.Count; i++)
            {
                if (String.Equals(purchaseEvent.purchasedProduct.definition.id,
                    purchasePreset.purchases[i].purchaseName,
                    StringComparison.Ordinal))
                {
                    OnPurchaseIsSucceeded?.Invoke(purchasePreset.purchases[i].purchaseCoinsContent);
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product '{0}'",
                        purchaseEvent.purchasedProduct.definition.id));
                }
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}'," +
                                    " PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }
}
