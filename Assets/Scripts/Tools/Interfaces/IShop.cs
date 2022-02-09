using System;
using Tools;


    public interface IShop
    {
        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchase();
        //IReadOnlySubscriptionActionT<string> OnSuccessPurchase { get; }

        IReadOnlySubscriptionAction OnSuccessPurchase { get; }
    //event Action<PurchaseInfo> OnSuccessPurchase;
    IReadOnlySubscriptionAction OnFailedPurchase { get; }

    }

//public enum ProductType
//{
//    Coins 
//}

//public struct PurchaseInfo
//{   
//    private string  productID;
//    public int Count;

//   public ProductType productType
//   {
//        get 
//        { 
//            switch(productID)
//            {
//                case "com.SMITandKo.racingGame.goldPack500":
//                    return ProductType.Coins;

//                default: throw new System.Exception("No found ID");


//            }
//        }

//   }
//        public PurchaseInfo(string ID, int count = 1)
//        {
//            productID = ID;
//             Count = count;
//        }
//}


