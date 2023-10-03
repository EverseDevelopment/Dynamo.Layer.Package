using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Views
{
    public class GetCategoryViewDetails
    {
        // Empty constructor to avoid import in Dynamo
        private GetCategoryViewDetails() { }

        public static Dictionary<string, object> GetViewDetails(string projectId, string categoryId, string viewId, string bearerToken)
        {
            RestClientOptions options = new RestClientOptions();
            options.MaxTimeout = -1;
            var client = new RestClient(options);

            var request = new RestRequest($"https://api.layer.team/projects/{projectId}", Method.Get);

            // Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var viewDetailResponse = JsonConvert.DeserializeObject<ViewDetailResponse>(response.Content);

                    // Map view details
                    var result = new Dictionary<string, object>
                    {
                        { "createdAt", viewDetailResponse.View.CreatedAt.ToString() },
                        { "id", viewDetailResponse.View.Id },
                        { "name", viewDetailResponse.View.Name },
                        { "type", viewDetailResponse.View.Type },
                        { "updatedAt", viewDetailResponse.View.UpdatedAt.ToString() }
                    };

                    // Map field display
                    var fieldDisplayList = new List<Dictionary<string, string>>();
                    foreach (var field in viewDetailResponse.View.FieldDisplay)
                    {
                        var fieldDict = new Dictionary<string, string>
                        {
                            { "id", field.Id },
                            { "type", field.Type }
                        };
                        fieldDisplayList.Add(fieldDict);
                    }

                    result["fieldDisplay"] = fieldDisplayList;
                    result["filterConfig"] = new Dictionary<string, object>
                    {
                        { "condition", viewDetailResponse.View.FilterConfig.Condition },
                        { "filters", viewDetailResponse.View.FilterConfig.Filters }
                    };
                    result["sort"] = viewDetailResponse.View.Sort;

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
