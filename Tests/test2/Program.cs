using System;
using System.Threading.Tasks;
using System.Collections.Generic;

class Pokemon
{
    public string Name { get; set; }
    public string Ability { get; set; }
    public string Type1 { get; set; }
    public string Type2 { get; set; }
}

class Box
{
    private List<Pokemon> pokeList = new List<Pokemon>();

    private async Task<Pokemon> GetPokemonAsync(int id)
    {
        using var client = new System.Net.Http.HttpClient();
        var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Erro ao buscar o Pokémon.");
        }

        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Pokemon>(await response.Content.ReadAsStringAsync());
        return data;
    }

    public async Task AddPokemonAsync(int id)
    {
        try
        {
            var data = await GetPokemonAsync(id);

            if (data != null)
            {
                pokeList.Add(data);
                Console.WriteLine($"{data.Name} adicionado à caixa");
            }
            else
            {
                Console.WriteLine("Pokémon não encontrado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void ListPokemon()
    {
        foreach (var pokemon in pokeList)
        {
            Console.WriteLine($"Nome: {pokemon.Name} | Habilidade: {pokemon.Ability} | Tipo 1: {pokemon.Type1} | Tipo 2: {pokemon.Type2}");
        }
    }

    public void RemovePokemon(int i)
    {
        if (i >= 0 && i < pokeList.Count)
        {
            var removedPokemon = pokeList[i];
            pokeList.RemoveAt(i);
            Console.WriteLine($"Pokémon removido: {removedPokemon.Name}");
        }
        else
        {
            Console.WriteLine("Pokémon não encontrado");
        }
    }

    public async Task UpdatePokemonAsync(int i, int newId)
    {
        if (i >= 0 && i < pokeList.Count)
        {
            try
            {
                var data = await GetPokemonAsync(newId);

                if (data != null)
                {
                    var updatedPokemon = new Pokemon
                    {
                        Name = data.Name,
                        Ability = data.Ability,
                        Type1 = data.Type1,
                        Type2 = data.Type2
                    };

                    pokeList[i] = updatedPokemon;
                    Console.WriteLine($"Pokémon na posição {i} foi atualizado para {updatedPokemon.Name}");
                }
                else
                {
                    Console.WriteLine("Pokémon não encontrado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Pokémon não encontrado na posição especificada");
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var box1 = new Box();
        await box1.AddPokemonAsync(3);
        await box1.AddPokemonAsync(6);
        await box1.AddPokemonAsync(9);
        await box1.AddPokemonAsync(10);

        await box1.UpdatePokemonAsync(3, 150);

        box1.ListPokemon();
    }
}