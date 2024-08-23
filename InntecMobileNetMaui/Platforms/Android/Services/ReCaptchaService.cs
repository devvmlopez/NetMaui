﻿using System.Threading.Tasks;
using Android.Content;
using Plugin.CurrentActivity;
using Android.Gms.SafetyNet;
using InntecMobile.Droid.Services;
using InntecMobileNetMaui.Services;

//[assembly: Xamarin.Forms.Dependency(typeof(ReCaptchaService))]
[assembly: Dependency(typeof(ReCaptchaService))]
namespace InntecMobile.Droid.Services
{
    public class ReCaptchaService : IReCaptchaService
    {
        private static Context CurrentContext => CrossCurrentActivity.Current.Activity;
        private SafetyNetClient _safetyNetClient;

        public SafetyNetClient SafetyNetClient
        {
            get { return _safetyNetClient ??= SafetyNetClass.GetClient(CurrentContext); }
        }


        public async Task<string> Verify(string siteKey, string domainUrl)
        {
            SafetyNetApiRecaptchaTokenResponse response = await SafetyNetClass.GetClient(CrossCurrentActivity.Current.Activity).VerifyWithRecaptchaAsync(siteKey);

            if(response == null) { return null; }
            else

            return response?.TokenResult;
        }
    }
}