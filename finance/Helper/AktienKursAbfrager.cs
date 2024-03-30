using System.Text.Json;

namespace finance.Helper;

public class AktienKursAbfrager
{

    private static string apiKey = "3nqsfES99bh9PF02I4GjPn1DoHXz73Js";
    private static string baseurl = "https://financialmodelingprep.com/api/v3/search";
    private static string pquery = "query";
    private static string pkey = "apikey";
    


    public async Task<string> GetStocks(string search)
    {
        string url = $"{baseurl}?{pquery}={search}&{pkey}={apiKey}";
        string jsonResponse = await MakeRequest(url);

        // You can process the jsonResponse here as needed

        return jsonResponse; // Modify the return value based on your requirements
    }

    public decimal GetStockQuote(string symbol)
    {
        return 10.23m;
        string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}";
       // string jsonResponse =  MakeRequest(url);

        // Using System.Text.Json for JSON parsing (available in .NET Core 3.0+)
        //var jsonDoc = JsonDocument.Parse(jsonResponse);

        try
        {
          //  string quote = jsonDoc.RootElement.GetProperty("Global Quote").GetProperty("05. price").GetString();
            //decimal stockQuote = decimal.Parse(quote);
            return 0;
            //return stockQuote;
        }
        catch
        {
            return 0;
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