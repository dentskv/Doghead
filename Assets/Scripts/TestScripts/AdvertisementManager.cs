using System;
using UnityEngine;
using GoogleMobileAds.Api;
using Zenject;

public class AdvertisementManager : IInitializable, IDisposable
{
    private RewardedAd _rewardedAd;
    private AdRequest _request; 
    
    public event Action<float> OnEarnedReward;

    public void Initialize()
    {
        MobileAds.Initialize(status => {
            this._rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
            this._rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            this._rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            this._rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            this._rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            _request = new AdRequest.Builder().Build();
            LoadRequest();
        });
    }

    public void Dispose()
    {
        this._rewardedAd.OnAdLoaded -= HandleRewardedAdLoaded;
        this._rewardedAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
        this._rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
        this._rewardedAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
        this._rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        this._rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }

    private void LoadRequest()
    {
        this._rewardedAd.LoadAd(_request);
    }
    
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad event received with message: " + args.LoadAdError);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        LoadRequest();
        Debug.Log("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        OnEarnedReward?.Invoke(100);
        Debug.Log("HandleRewardedAdRewarded event received for 100 coins");
    }
    
    public void UserChoseToWatchAd()
    {
        if (this._rewardedAd.IsLoaded()) {
            this._rewardedAd.Show();
        }
        else
        {
            LoadRequest();
        }
    }
}
