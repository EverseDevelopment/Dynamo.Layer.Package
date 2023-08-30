using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Categories
{
    public class GetProjectCategories
    {
        // Empty constructor to avoid import in Dynamo
        private GetProjectCategories() { }

        public static List<Dictionary<string, string>> GetCategoriesByProjectId(string projectId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/categories");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            // Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var categoriesResponse = JsonConvert.DeserializeObject<CategoriesResponse>(response.Content);
                    var categoriesList = new List<Dictionary<string, string>>();

                    foreach (var category in categoriesResponse.categories)
                    {
                        var categoryDict = new Dictionary<string, string>
                        {
                            { "createdAt", category.createdAt.ToString() },
                            { "count", category.count.ToString() },
                            { "initials", category.initials },
                            { "instance", category.instance },
                            { "modelCategory", category.modelCategory.ToString() },
                            { "name", category.name },
                            { "id", category.id },
                            { "updatedAt", category.updatedAt.ToString() }
                        };

                        if (category.order.HasValue)
                        {
                            categoryDict["order"] = category.order.Value.ToString();
                        }

                        categoriesList.Add(categoryDict);
                    }

                    return categoriesList;
                }
                else
                {
                    // Handle non-successful response
                    throw new Exception($"API call failed with status: {response.StatusCode}, content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }

    class CategoriesResponse
    {
        public List<Category> categories { get; set; }
    }

    class Category
    {
        public DateTime createdAt { get; set; }
        public int count { get; set; }
        public string initials { get; set; }
        public string instance { get; set; }
        public bool modelCategory { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public DateTime updatedAt { get; set; }
        public int? order { get; set; } // Order property can be null hence marked as nullable
    }
}

