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

            RestClientOptions options = new RestClientOptions();
            options.MaxTimeout = -1;
            var client = new RestClient(options);

            var request = new RestRequest($"https://api.layer.team/projects/{projectId}/categories/{categoryId}", Method.Get);
            // Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var categoryDetailResponse = JsonConvert.DeserializeObject<CategoryDetailResponse>(response.Content);

                    // Map category details
                    var result = new Dictionary<string, object>
                    {
                        { "createdAt", categoryDetailResponse.Category.CreatedAt.ToString() },
                        { "count", categoryDetailResponse.Category.Count.ToString() },
                        { "initials", categoryDetailResponse.Category.Initials },
                        { "instance", categoryDetailResponse.Category.Instance },
                        { "modelCategory", categoryDetailResponse.Category.ModelCategory.ToString() },
                        { "name", categoryDetailResponse.Category.Name },
                        { "id", categoryDetailResponse.Category.Id },
                        { "updatedAt", categoryDetailResponse.Category.UpdatedAt.ToString() }
                    };

                    // Map views
                    var viewsList = new List<Dictionary<string, string>>();
                    foreach (var view in categoryDetailResponse.Views)
                    {
                        var viewDict = new Dictionary<string, string>
                        {
                            { "id", view.Id },
                            { "name", view.Name },
                            { "type", view.Type }
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
}
