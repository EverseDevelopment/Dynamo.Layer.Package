using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Elements
{
    public class GetAllElements
    {
        // Empty constructor to avoid import in Dynamo
        private GetAllElements() { }

        public static List<Dictionary<string, string>> GetElements(string projectId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/elements");
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
                    // Deserialize response to the list of Element objects
                    var elementsList = JsonConvert.DeserializeObject<ElementResponse>(response.Content);
                    var elementsDictList = new List<Dictionary<string, string>>();

                    foreach (var element in elementsList.Elements)
                    {
                        var elementDict = new Dictionary<string, string>
                        {
                            { "autoIncrementId", element.AutoIncrementId.ToString() },
                            { "completed", element.Completed.ToString() },
                            { "createdAt", element.CreatedAt.ToString() },
                            { "createdBy", element.CreatedBy },
                            { "id", element.Id },
                            { "modelRevitId", element.ModelRevitId },
                            { "name", element.Name },
                            { "starred", element.Starred.ToString() },
                            { "status", element.Status },
                            { "updatedAt", element.UpdatedAt.ToString() },
                            { "updatedBy", element.UpdatedBy }
                            // Add other fields as necessary
                        };

                        // If you want to include nested fields, you'll have to serialize them back to JSON or somehow convert them to a string
                        elementDict["category"] = JsonConvert.SerializeObject(element.Category);
                        elementDict["fields"] = JsonConvert.SerializeObject(element.Fields);
                        elementDict["params"] = JsonConvert.SerializeObject(element.Params);
                        elementDict["spatialRelationships"] = JsonConvert.SerializeObject(element.SpatialRelationships);

                        elementsDictList.Add(elementDict);
                    }

                    return elementsDictList;
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
