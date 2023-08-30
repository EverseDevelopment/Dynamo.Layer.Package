using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Categories
{
    public class GetCategoryDetails
    {
        // Empty constructor to avoid import in Dynamo
        private GetCategoryDetails() { }

        public static Dictionary<string, object> GetCategoryDetailsById(string projectId, string categoryId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/categories/{categoryId}");
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
                    var categoryDetailResponse = JsonConvert.DeserializeObject<CategoryDetailResponse>(response.Content);

                    // Map category details
                    var result = new Dictionary<string, object>
                    {
                        { "createdAt", categoryDetailResponse.category.createdAt.ToString() },
                        { "count", categoryDetailResponse.category.count.ToString() },
                        { "initials", categoryDetailResponse.category.initials },
                        { "instance", categoryDetailResponse.category.instance },
                        { "modelCategory", categoryDetailResponse.category.modelCategory.ToString() },
                        { "name", categoryDetailResponse.category.name },
                        { "id", categoryDetailResponse.category.id },
                        { "updatedAt", categoryDetailResponse.category.updatedAt.ToString() }
                    };

                    // Map views
                    var viewsList = new List<Dictionary<string, string>>();
                    foreach (var view in categoryDetailResponse.views)
                    {
                        var viewDict = new Dictionary<string, string>
                        {
                            { "id", view.id },
                            { "name", view.name },
                            { "type", view.type }
                        };
                        viewsList.Add(viewDict);
                    }

                    result["views"] = viewsList;

                    return result;
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

    class CategoryDetailResponse
    {
        public Category category { get; set; }
        public List<View> views { get; set; }
    }

    class View
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}
