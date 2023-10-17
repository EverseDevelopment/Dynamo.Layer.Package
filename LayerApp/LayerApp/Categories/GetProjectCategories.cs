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
                    var categoriesResponse = JsonConvert.DeserializeObject<CategoryResponse>(response.Content);
                    var categoriesList = new List<Dictionary<string, string>>();

                    foreach (var category in categoriesResponse.Categories)
                    {
                        var categoryDict = new Dictionary<string, string>
                        {
                            { "createdAt", category.CreatedAt.ToString() },
                            { "count", category.Count.ToString() },
                            { "initials", category.Initials },
                            { "instance", category.Instance },
                            { "modelCategory", category.ModelCategory.ToString() },
                            { "name", category.Name },
                            { "id", category.Id },
                            { "updatedAt", category.UpdatedAt.ToString() }
                        };

                        if (category.Order.HasValue)
                        {
                            categoryDict["order"] = category.Order.Value.ToString();
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
}

