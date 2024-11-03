using Newtonsoft.Json;
using System.Net.Http.Headers;

public class Program
{
    public async static Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public async static Task<int> getTotalScoredGoals(string team, int year)
    {
        if (string.IsNullOrEmpty(team))
            return 0;

        int totalGoals = 0;
        var url = $"https://jsonmock.hackerrank.com/api/football_matches?team1={team}&year={year}";
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<retorno>(content);
            totalGoals += item.data.Sum(x => Convert.ToInt32(x.team1goals));
        }

        var url2 = $"https://jsonmock.hackerrank.com/api/football_matches?team2={team}&year={year}";
        var response2 = await client.GetAsync(url);
        if (response2.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<retorno>(content);
            totalGoals += item.data.Sum(x => Convert.ToInt32(x.team2goals));
        }
        return totalGoals;
    }

    public class retorno { 
        public int? page { get; set; }
        public int? per_page { get; set; }
        public int? total { get; set; }
        public int? total_pages { get; set; }
        public List<data> data { get; set; }

    }
    public class data
    {

        public string? competition { get; set; }
        public int? year { get; set; }
        public string? round { get; set; }
        public string? team1 { get; set; }
        public string? team2 { get; set; }
        public string? team1goals { get; set; }
        public string? team2goals { get; set; }
        
    }

}