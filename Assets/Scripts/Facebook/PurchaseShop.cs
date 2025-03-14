using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseShop : MonoBehaviour, IStoreListener
{
	private static IStoreController m_StoreController;                                                                  // Reference to the Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;                                                         // Reference to store-specific Purchasing subsystems.

	// Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
	// for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

	private static string kProductIDConsumable1 =    "consumable1";  
	private static string kProductIDConsumable2 =    "consumable2"; 
	private static string kProductIDConsumable3 =    "consumable3"; 
	private static string kProductIDConsumable4 =    "consumable4"; 
	private static string kProductIDConsumable5 =    "consumable5"; // General handle for the consumable product.
	private static string kProductIDNonConsumable = "nonconsumable";                                                  // General handle for the non-consumable product.
	private static string kProductIDSubscription =  "subscription";                                                   // General handle for the subscription product.

	private static string kProductNameAppleConsumable1 =    "com.vietbrain.SuperKongSaga.1000golds";             // Apple App Store identifier for the consumable product.
	private static string kProductNameAppleConsumable2 =    "com.vietbrain.SuperKongSaga.2000golds";
	private static string kProductNameAppleConsumable3 =    "com.vietbrain.SuperKongSaga.3000golds";
	private static string kProductNameAppleConsumable4 =    "com.vietbrain.SuperKongSaga.5000golds";
	private static string kProductNameAppleConsumable5 =    "com.vietbrain.SuperKongSaga.10000golds";
	private static string kProductNameAppleNonConsumable = "com.unity3d.test.services.purchasing.nonconsumable";      // Apple App Store identifier for the non-consumable product.
	private static string kProductNameAppleSubscription =  "com.unity3d.test.services.purchasing.subscription";       // Apple App Store identifier for the subscription product.

	private static string kProductNameGooglePlayConsumable1 =    "goldx1000";        // Google Play Store identifier for the consumable product.
	private static string kProductNameGooglePlayConsumable2 =    "goldx2000";
	private static string kProductNameGooglePlayConsumable3 =    "goldx3000";
	private static string kProductNameGooglePlayConsumable4 =    "goldx5000";
	private static string kProductNameGooglePlayConsumable5 =    "goldx10000";
	private static string kProductNameGooglePlayNonConsumable = "com.unity3d.test.services.purchasing.nonconsumable";     // Google Play Store identifier for the non-consumable product.
	private static string kProductNameGooglePlaySubscription =  "com.unity3d.test.services.purchasing.subscription";  // Google Play Store identifier for the subscription product.

	void Start()
	{
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.

		builder.AddProduct(kProductIDConsumable1, ProductType.Consumable, new IDs(){{ kProductNameAppleConsumable1,       AppleAppStore.Name },{ kProductNameGooglePlayConsumable1,  GooglePlay.Name },});// Continue adding the non-consumable product.
		builder.AddProduct(kProductIDConsumable2, ProductType.Consumable, new IDs(){{ kProductNameAppleConsumable2,       AppleAppStore.Name },{ kProductNameGooglePlayConsumable2,  GooglePlay.Name },});
		builder.AddProduct(kProductIDConsumable3, ProductType.Consumable, new IDs(){{ kProductNameAppleConsumable3,       AppleAppStore.Name },{ kProductNameGooglePlayConsumable3,  GooglePlay.Name },});
		builder.AddProduct(kProductIDConsumable4, ProductType.Consumable, new IDs(){{ kProductNameAppleConsumable4,       AppleAppStore.Name },{ kProductNameGooglePlayConsumable4,  GooglePlay.Name },});
		builder.AddProduct(kProductIDConsumable5, ProductType.Consumable, new IDs(){{ kProductNameAppleConsumable5,       AppleAppStore.Name },{ kProductNameGooglePlayConsumable5,  GooglePlay.Name },});
		//builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable, new IDs(){{ kProductNameAppleNonConsumable,       AppleAppStore.Name },{ kProductNameGooglePlayNonConsumable,  GooglePlay.Name },});// And finish adding the subscription product.
		//builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){{ kProductNameAppleSubscription,       AppleAppStore.Name },{ kProductNameGooglePlaySubscription,  GooglePlay.Name },});// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyConsumable1()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable1);
	}

	public void BuyConsumable2()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable2);
	}

	public void BuyConsumable3()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable3);
	}

	public void BuyConsumable4()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable4);
	}

	public void BuyConsumable5()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDConsumable5);
	}

	public void BuyNonConsumable()
	{
		// Buy the non-consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDNonConsumable);
	}


	public void BuySubscription()
	{
		// Buy the subscription product using its the general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDSubscription);
	}


	void BuyProductID(string productId)
	{
		foreach(var item in m_StoreController.products.all)
		{
			print (m_StoreController.products.all.Length);
		}
		// If the stores throw an unexpected exception, use try..catch to protect my logic here.
		try
		{
			// If Purchasing has been initialized ...
			if (IsInitialized())
			{
				// ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
				Product product = m_StoreController.products.WithID(productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				if (product != null && product.availableToPurchase)
				{
					Debug.Log (string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
					Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		// Complete the unexpected exception handling ...
		catch (Exception e)
		{
			// ... by reporting any unexpected exception for later diagnosis.
			Debug.Log ("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable1, StringComparison.Ordinal))
		{
			//Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			ItemManager.AddGold (1000);
			Debug.Log ("+1000");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable2, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			ItemManager.AddGold (2000);
			Debug.Log ("+2000");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable3, StringComparison.Ordinal))
			{
				Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			ItemManager.AddGold (3000);
			Debug.Log ("+3000");
			}
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable4, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			ItemManager.AddGold (5000);
			Debug.Log ("+5000");
		}
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable5, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			ItemManager.AddGold (10000);
			Debug.Log ("+10000");
		}

		// Or ... a non-consumable product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));}// Or ... a subscription product has been purchased by this user.
		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));}// Or ... an unknown product has been purchased by this user. Fill in additional products here.
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));}// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",product.definition.storeSpecificId, failureReason));}

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        
    }
}
