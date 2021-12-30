using api_test.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace api_test.Services;

public class PizzaService
{
    // static List<Pizza> Pizzas { get; }
    // static int nextId = 3;
    private readonly IMongoCollection<Pizza> _pizzasCollection;
    /*
    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
        };
    }
    */
    public PizzaService(IOptions<PizzaDatabaseSettings> pizzaDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            pizzaDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            pizzaDatabaseSettings.Value.DatabaseName);

        _pizzasCollection = mongoDatabase.GetCollection<Pizza>(
            pizzaDatabaseSettings.Value.PizzasCollectionName);
    }

    public async Task<List<Pizza>> GetAll() =>
        await _pizzasCollection.Find(_ => true).ToListAsync();

    public async Task<Pizza?> Get(string id) =>
        await _pizzasCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Add(Pizza pizza) =>
        await _pizzasCollection.InsertOneAsync(pizza);

    public async Task Delete(string id) =>
        await _pizzasCollection.DeleteOneAsync(x => x.Id == id);

    public async Task Update(Pizza updated_pizza) =>
        await _pizzasCollection.ReplaceOneAsync(x => x.Id == updated_pizza.Id, updated_pizza);
}