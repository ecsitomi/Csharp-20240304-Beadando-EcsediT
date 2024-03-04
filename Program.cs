using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            //Beolvasás
            string apiUrl = "https://retoolapi.dev/Kc6xuH/data";
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(apiUrl))
            { 
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Hiba: {response.StatusCode}");
                    return;
                }
                var jsonBeerkezett = await response.Content.ReadAsStringAsync();
                //var elemek = new List<Elem>(); -ha van előzetes lista akkor az alatti sor hibás
                var elemek = JsonSerializer.Deserialize<List<Elem>>(jsonBeerkezett);
                //beolvasás vége

                Console.WriteLine($"Elemek száma: {elemek.Count}"); //első feladat

                var legmagasabb = 0; //második feladat
                foreach ( var elem in elemek )
                {
                    if (elem.salary > legmagasabb )
                    {
                        legmagasabb = elem.salary;
                    }
                }
                Console.WriteLine($"Legmagasabb fizetés: {legmagasabb}");

                //harmadik feladat
                Dictionary<string, int> poziPerFo = new Dictionary<string, int>();
                foreach ( var elem in elemek )
                {
                    if (poziPerFo.ContainsKey(elem.position)) //ha már van
                    {
                        poziPerFo[elem.position]++; //hozzáad egyet
                    }
                    else
                    {
                        poziPerFo.Add(elem.position, 1); //egyről indul
                    }
                }
                foreach ( var arany in poziPerFo)
                {
                    Console.WriteLine($"{poziPerFo.Keys}: {poziPerFo.Values}"); //pozi - db kiírás
                }
            }
        }
        class Elem //Objektum 
        {
            [JsonPropertyName("id")]
            public int id { get; set; }
            [JsonPropertyName("name")]
            public string name { get; set; }

            [JsonPropertyName("salary")]
            public int salary { get; set; }

            [JsonPropertyName("positon")]
            public string position { get; set; }
        }
    }
}
