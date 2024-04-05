using System.Text.Json;
using System.Text.RegularExpressions;
using finance.Models;
using Newtonsoft.Json;

namespace finance.Helper;


public class AktienKursAbfrager
{

    private static string apiKey = "3nqsfES99bh9PF02I4GjPn1DoHXz73Js";
    private static string baseurl = "https://financialmodelingprep.com/api/v3";
    private static string pquery = "query";
    private static string pkey = "apikey";
    


    public async Task<string> GetStocks(string search)
    {
        string url = $"{baseurl}/search?{pquery}={search}&{pkey}={apiKey}";
        string jsonResponse = await MakeRequest(url);

        // You can process the jsonResponse here as needed

        return jsonResponse; // Modify the return value based on your requirements
    }

    public async Task<QuoteResult?> GetStockQuote(string symbol)
    {

        string url = $"{baseurl}/quote-order/{symbol}?{pkey}={apiKey}";
        try
        {
        string json = await MakeRequest(url);
        json = Regex.Replace(json, @"\t|\n|\r", "");

        // Using System.Text.Json for JSON parsing (available in .NET Core 3.0+)
      

            List<QuoteResult> stockPrices = JsonConvert.DeserializeObject<List<QuoteResult>>(json);

            return stockPrices[0];
        }
        catch(Exception e)
        {
            return new QuoteResult()
            {
                name = "Limit erreicht",
                symbol = "LIM",
                price = 100
            };
        }
        
    }
    
    private async Task<string> MakeRequest(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException("Failed to retrieve data from the API");
            }
        }
    }
    

}