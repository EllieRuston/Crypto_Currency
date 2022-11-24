using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Cripto_C.Models;
using System.Net.Http;
using Microsoft.AppCenter.Crashes;

namespace Cripto_C
{
    public partial class MainPage : ContentPage
    {
        private string ApiKey = "AF723AA0-78DD-4B31-818F-389085C48436";
        private string base_Icon_Url = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_256/";


        public MainPage()
        {
            InitializeComponent();
            cList.ItemsSource = GetCryp();
        }

        private List<Cryp_Coin> GetCryp()
        {
            List<Cryp_Coin> crypto;
            // call 5 crypto currencies
            var coin_api = new RestClient("http://rest.coinapi.io/v1/assets/DOT;ZEC;TRX;USDT;ENJ");
            var request = new RestRequest();
            // validation key
            request.AddHeader("X-CoinAPI-Key", ApiKey);
            var response = coin_api.Execute(request);
            crypto = JsonConvert.DeserializeObject<List<Cryp_Coin>>(response.Content);
            // get icon for each currency
            foreach (var c in crypto)
            {
                if (!string.IsNullOrEmpty(c.Id_icon))
                    c.Icon_url = base_Icon_Url + c.Id_icon.Replace("-", "") + ".png";
                else
                    c.Icon_url = "";
            }
            return crypto;

        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            // App Centre Analytics data
            try
            {
                Crashes.GenerateTestCrash();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
            // call new data for crypto currency
            cList.ItemsSource = GetCryp();

        }   
    }
}
