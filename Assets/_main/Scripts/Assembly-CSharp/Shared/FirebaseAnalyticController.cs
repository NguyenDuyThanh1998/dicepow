using AppsFlyerSDK;

using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseAnalyticController : SingletonDestroyOnLoad<FirebaseAnalyticController>
{
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        Debug.Log("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        Debug.Log("Set user properties.");
        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod,"Google");
        //FirebaseAnalytics.SetUserId("uber_user_510");
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;

        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;

        AnalyticsLogin();
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        StartCoroutine(LogOut(sender,token));
    }

    private IEnumerator LogOut(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {

        yield return new WaitUntil(() => AppsFlyer.instance != null);

#if UNITY_ANDROID
        ((AppsFlyerAndroid)AppsFlyer.instance).updateServerUninstallToken(token.Token);
#endif
    }

    public void AnalyticsLogin()
    {
        // Log an event with no parameters.
        Debug.Log("Logging a login event.");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }

    public static Parameter[] GetParameter(Dictionary<string,string> par)
    {
        var p = new Parameter[par?.Count ?? 0];

        if (par != null)
        {
            for (int i = 0; i < par.Count; i++)
            {
                var data = par.ElementAt(i);
                p[i] = new Parameter(data.Key, data.Value);
            }

        }

        return p;
    }

}
