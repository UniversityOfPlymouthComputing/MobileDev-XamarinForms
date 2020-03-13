using System;
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
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/IsExplored", 400);

            //Add initial records
            SolPlanet earth = new SolPlanet("Earth", 400.0, true);

            try
            {
                // Read the item to see if it exists.  
                ItemResponse<SolPlanet> earthResponse = await this.container.ReadItemAsync<SolPlanet>(earth.Name, new PartitionKey(earth.IsExplored));
                Console.WriteLine("Item in database with id: {0} already exists\n", earthResponse.Resource.Name);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<SolPlanet> earthResponse = await this.container.CreateItemAsync<SolPlanet>(earth, new PartitionKey(earth.IsExplored));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", earthResponse.Resource.Name, earthResponse.RequestCharge);
            }


        }


    }
}
