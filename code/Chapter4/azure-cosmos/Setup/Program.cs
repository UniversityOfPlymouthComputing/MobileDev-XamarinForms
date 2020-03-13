using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Setup
{

    public class Program
    {
        private static readonly string EndpointUri = Environment.GetEnvironmentVariable("AzureUri");

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = Environment.GetEnvironmentVariable("AzureKey");

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "ListOfPlanetsDB";
        private string containerId = "Items";

        public static async Task Main(string[] args)
        {
            var p = new Program();
            await p.Go();
        }

        public async Task Go()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() {
                ApplicationName = "ListOfPlanets"
            } );

            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);

            //Create container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Orbits", 400);

            //Add initial records
            await AddPlanetIfDoesNotExist(new SolPlanet("Mercury", 57.0));
            await AddPlanetIfDoesNotExist(new SolPlanet("Venus", 108.0));
            await AddPlanetIfDoesNotExist(new SolPlanet("Earth", 150.0, true));
            await AddPlanetIfDoesNotExist(new SolPlanet("Mars", 228.0));       
            await AddPlanetIfDoesNotExist(new SolPlanet("Jupiter", 779.0));
            await AddPlanetIfDoesNotExist(new SolPlanet("Saturn", 1430.0));
            await AddPlanetIfDoesNotExist(new SolPlanet("Uranus", 2880.0));
            await AddPlanetIfDoesNotExist(new SolPlanet("Nepture", 4500.0));
            await AddPlanetIfDoesNotExist(new SolPlanet("Pluto", 5910.0));

            //Read back all records        
            await QueryAllRecords(true);
            await QueryAllRecords(false);

            //Update record
            var mars = new SolPlanet("Mars", 228, true);
            await ToggleExplored("Mars");

            //Delete record
            await DeleteItemAsync("Uranus");
        }

        async Task AddPlanetIfDoesNotExist(SolPlanet p)
        {
            try
            {
                // Read the item to see if it exists.  The ID (unique) is Name property
                ItemResponse<SolPlanet> planetResponse = await this.container.ReadItemAsync<SolPlanet>(p.Name, new PartitionKey(p.Orbits));
                Console.WriteLine("Item in database with id: {0} already exists\n", planetResponse.Resource.Name);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<SolPlanet> planetResponse = await this.container.CreateItemAsync<SolPlanet>(p, new PartitionKey(p.Orbits));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", planetResponse.Resource.Name, planetResponse.RequestCharge);
            }
        }

        async Task QueryAllRecords(bool exp)
        {
            Console.WriteLine($"Explored is {exp}");
            var sql = $"SELECT * FROM c where c.IsExplored = {exp.ToString().ToLower()}";
            Console.WriteLine("Running query: {0}\n", sql);
            QueryDefinition queryDefinition = new QueryDefinition(sql);
            FeedIterator<SolPlanet> queryResultSetIterator = this.container.GetItemQueryIterator<SolPlanet>(queryDefinition);

            List<SolPlanet> planets = new List<SolPlanet>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<SolPlanet> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (SolPlanet planet in currentResultSet)
                {
                    planets.Add(planet);
                    Console.WriteLine("\tRead {0}\n", planet);
                }
            }
        }

        //Update
        async Task ToggleExplored(string name)
        {
            ItemResponse<SolPlanet> resp = await this.container.ReadItemAsync<SolPlanet>(name, new PartitionKey("Sol"));
            SolPlanet itemBody = resp.Resource;
            itemBody.IsExplored = !itemBody.IsExplored;
            resp = await container.ReplaceItemAsync<SolPlanet>(itemBody, itemBody.Name, new PartitionKey("Sol"));
            Console.WriteLine($"Updated {name} - explored set up {itemBody.IsExplored} - response {resp}");
        }

        //Delete
        private async Task DeleteItemAsync(string name)
        {
            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<SolPlanet> resp = await this.container.DeleteItemAsync<SolPlanet>(name, new PartitionKey("Sol"));
            Console.WriteLine($"Deleted {name} - response {resp}");
        }

        //Delete Everything
        private async Task DeleteDatabaseAndCleanupAsync()
        {
            DatabaseResponse databaseResourceResponse = await this.database.DeleteAsync();
            // Also valid: await this.cosmosClient.Databases["FamilyDatabase"].DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", this.databaseId);

            //Dispose of CosmosClient
            this.cosmosClient.Dispose();
        }

    }
}
