using System.Collections.Generic;
using UnityEngine;

using Firebase.Analytics;
using AppsFlyerSDK;
using System.Collections;

public class EventTracking : SingletonDestroyOnLoad<EventTracking>
{
	public const string ads_inter_loaded = "{0}_ads_inter_loaded";
	public const string ads_inter_show = "{0}_ads_inter_show";
	public const string ads_inter_fail = "{0}_ads_inter_fail";
	public const string ads_inter_click = "{0}_ads_inter_loaded";
	public const string ads_ad_impression = "{0}_ad_impression";
	public const string ads_clicked = "{0}_ads_inter_click";
	public const string ads_inter_fail_to_showed = "{0}_ads_inter_fail_to_showed";

	public const string LEVEL_PLAY = "{0}_wave_spawn_{1}_{2}";

	private const string platform_af = "af";
	private const string platform_fb = "fbs";

	// Use this for initialization
	void Start()
	{
		AppsFlyer.setIsDebug(false);
		Application.runInBackground = true;

		AdsMAXManager.Instance.SetInterLoaded(SentEventAdsInterLoaded);
		AdsMAXManager.Instance.SetInterShowed(SentEventAdsInterShowed);
		AdsMAXManager.Instance.SetInterShowed(SentEventAdsInterClick);
		AdsMAXManager.Instance.SetInterFailed(SentEventAdsInterFailed);
		AdsMAXManager.Instance.SetInterFailToShow(SentEventAdsInterFailedToShow);

		StartCoroutine(InitializeEvent());
	}

	private IEnumerator InitializeEvent()
    {
		yield return new WaitForSeconds(1f);
		AppOpen();
		AdsImpression();
	}

	private void SentEvent(string mainKey, Dictionary<string, string> data)
	{
		AppsFlyer.sendEvent(string.Format(mainKey, platform_af), data);
		FirebaseAnalytics.LogEvent(string.Format(mainKey, platform_fb), FirebaseAnalyticController.GetParameter(data));
	}

	public void EventFinish(string wave, string mode)
    {
        var eventValues = GetDefault();
		var af = string.Format(LEVEL_PLAY, platform_af, mode, wave);
		var fb = string.Format(LEVEL_PLAY, platform_fb, mode, wave);

		AppsFlyer.sendEvent(af, eventValues);
		FirebaseAnalytics.LogEvent(fb, FirebaseAnalyticController.GetParameter(eventValues));
    }

    public void AppOpen()
    {
		AppsFlyer.sendEvent(AFInAppEvents.LOGIN, new Dictionary<string, string>());
	}

	public void AdsImpression()
    {
		var eventValues = new Dictionary<string, string>();
		eventValues.Add("MaxSdkKey", AdsMAXManager.Instance.MaxSdkKey);
		eventValues.Add("InterstitialAdUnitId", AdsMAXManager.Instance.InterstitialAdUnitId);
		eventValues.Add("MaRewardedAdUnitIdxSdkKey", AdsMAXManager.Instance.RewardedAdUnitId);
		eventValues.Add("RewardedInterstitialAdUnitId", AdsMAXManager.Instance.RewardedInterstitialAdUnitId);
		eventValues.Add("BannerAdUnitId", AdsMAXManager.Instance.BannerAdUnitId);
		eventValues.Add("MRecAdUnitId", AdsMAXManager.Instance.MRecAdUnitId);
		
		SentEvent(ads_ad_impression, eventValues);
	}

	public void SentEventAdsInterLoaded()
    {
		var eventValues = GetDefault();
		Debug.Log("Ads loaded");
		SentEvent(ads_inter_loaded, eventValues);

	}

	public void SentEventAdsInterShowed()
    {
		var eventValues = GetDefault();
		Debug.Log("Ads showed");

		SentEvent(ads_inter_show, eventValues);

		AdsImpression();
	}

	public void SentEventAdsInterClick()
	{
		var eventValues = GetDefault();
		Debug.Log("Ads showed");

		SentEvent(ads_clicked, eventValues);
	}

	public void SentEventAdsInterFailed()
    {
		var eventValues = GetDefault();
		Debug.Log("Ads failed");

		SentEvent(ads_inter_fail, eventValues);
	}

	public void SentEventAdsInterFailedToShow()
	{
		var eventValues = GetDefault();

		SentEvent(ads_inter_fail_to_showed, eventValues);
	}

	public Dictionary<string, string> GetDefault(bool defaultValue = true)
    {
		var eventValues = new Dictionary<string, string>();
        if (defaultValue)
        {
			eventValues.Add("customer_user_id", SystemInfo.deviceUniqueIdentifier);
		}
		return eventValues;
	}
}
