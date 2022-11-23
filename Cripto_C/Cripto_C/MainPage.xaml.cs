using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Cripto_C.Models;

namespace Cripto_C
{
    public partial class MainPage : ContentPage
    {
        private string ApiKey = "AF723AA0-78DD-4B31-818F-389085C48436";
        private string baseimageUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_256/";


        public MainPage()
        {
            InitializeComponent();
            cList.ItemsSource = GetCoins();
        }

        private List<Coin> GetCoins()
        {
            List<Coin> coins;

            var client = new RestClient("http://rest.coinapi.io/v1/assets/DOT;ZEC;TRX;USDT;ENJ");
            var request = new RestRequest();
            request.AddHeader("X-CoinAPI-Key", ApiKey);

            var response = client.Execute(request);
            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach (var c in coins)
            {
                if (!string.IsNullOrEmpty(c.Id_icon))
                    c.Icon_url = baseimageUrl + c.Id_icon.Replace("-", "") + ".png";
                else
                    c.Icon_url = "";
            }
            return coins;

        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            cList.ItemsSource = GetCoins();
        }
    }
}
