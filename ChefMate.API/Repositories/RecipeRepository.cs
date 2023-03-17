using ChefMate.API.Repositories.Interfaces;
using ChefMate.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ChefMate.API.Repositories;

public class RecipeRepository : IMongoRepository<Recipe>
    {
        private readonly IMongoClient _mongoClient;

        public RecipeRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }
        
        public async Task<IEnumerable<Recipe>> GetAll()
        {
            List<Recipe> RecipeCollection = await GetCollection().AsQueryable()
                .Where(u => !u.Deleted).ToListAsync();
            return RecipeCollection;
        }

        public async Task<Recipe> GetById(string id)
        {
            var Recipe = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.Id == id && !u.Deleted);
            return Recipe;
        }

        public async Task<Recipe> GetByOwnerId(string id)
        {
            var Recipe = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.OwnerId == id && !u.Deleted);
            return Recipe;
        }

        public async Task<Recipe> Create(Recipe data)
        {
            data.Id = Guid.NewGuid().ToString();
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            await GetCollection().InsertOneAsync(data);
            var RecipeList = await GetCollection().AsQueryable().ToListAsync();
            return RecipeList.FirstOrDefault(x => x.Id == data.Id);
        }

        public async Task<Recipe> Update(string id, Recipe data)
        {
            data.UpdatedAt = DateTime.UtcNow;
            data.Version++;
            await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
            return data;
        }

        public async Task Delete(string id, bool hardDelete = false)
        {
            if (hardDelete)
            {
                await GetCollection().DeleteOneAsync(x => x.Id == id);
            }
            else
            {
                await GetCollection().UpdateOneAsync<Recipe>(x => x.Id == id,
                    Builders<Recipe>.Update.Set(x => x.Deleted, true));
            }
        }
        
        private IMongoCollection<Recipe> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("ChefMate");
            IMongoCollection<Recipe> collection = database.GetCollection<Recipe>("Recipes");
            return collection;
        }
    }