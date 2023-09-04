using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Elements
{
    public class GetElements
    {
        // Empty constructor to avoid import in Dynamo
        private GetElements() { }

        public static List<Dictionary<string, object>> GetAllElements(string projectId, string bearerToken)
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
                ElementsResponse elementsList = JsonConvert.DeserializeObject<ElementsResponse>(response.Content);

                if (response.IsSuccessful && elementsList != null)
                {
                    var elementsDictList = new List<Dictionary<string, object>>();

                    foreach (var element in elementsList.Elements)
                    {
                        var elementDict = new Dictionary<string, object>
                        {
                            { "autoIncrementId", element.AutoIncrementId.ToString() },
                            { "completed", element.Completed.ToString() },
                            { "createdAt", element.CreatedAt },
                            { "createdBy", element.CreatedBy },
                            { "id", element.Id },
                            { "name", element.Name },
                            { "starred", element.Starred.ToString() },
                            { "status", element.Status },
                            { "updatedAt", element.UpdatedAt },
                            { "updatedBy", element.UpdatedBy },
                        };

                        var categoryDict = new Dictionary<string, string>
                        {
                            { "id", element.Category.Id },
                            { "name", element.Category.Name }
                        };
                        elementDict["category"] = categoryDict;

                        // Add 'params' nullable field
                        if (element.Params != null)
                        {
                            var paramsDict = new Dictionary<string, object>();
                            foreach (var param in element.Params)
                            {
                                var paramDetails = new Dictionary<string, string>
                                {
                                    { "name", param.Value.Name },
                                    { "parameterGroup", param.Value.ParameterGroup },
                                    { "revitId", param.Value.RevitId },
                                    { "scalar", param.Value.Scalar.ToString() },
                                    { "unit", param.Value.Unit },
                                    { "value", param.Value.Value }
                                };
                                paramsDict[param.Key] = paramDetails;
                            }
                            elementDict["params"] = paramsDict;
                        }

                        // Add 'spatialRelationships' nullable field
                        if (element.SpatialRelationships != null)
                        {
                            var spatialRelationshipsList = new List<Dictionary<string, string>>();
                            foreach (var spatialRelationship in element.SpatialRelationships)
                            {
                                var spatialDict = new Dictionary<string, string>
                            {
                                { "id", spatialRelationship.Id },
                                { "name", spatialRelationship.Name },
                                { "categoryId", spatialRelationship.CategoryId },
                                { "phaseName", spatialRelationship.PhaseName }
                            };
                                spatialRelationshipsList.Add(spatialDict);
                            }
                            elementDict["spatialRelationships"] = spatialRelationshipsList;
                        }

                        // Add 'fields' nullable field
                        if (element.Fields != null)
                        {
                            var fieldsDict = new Dictionary<string, object>();
                            foreach (var field in element.Fields)
                            {
                                var fieldDetails = new Dictionary<string, string>
                                {
                                    { "name", field.Value.Name },
                                    { "type", field.Value.Type },
                                    { "value", field.Value.Value.ToString() }
                                };
                                if (field.Value.Value != null) fieldDetails.Add("value", field.Value.Value.ToString());
                                fieldsDict[field.Key] = fieldDetails;
                            }
                            elementDict["fields"] = fieldsDict;
                        }

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

            catch (JsonSerializationException jsonEx)
            {
                Console.WriteLine($"JSON Serialization Error: {jsonEx.Message}");
                throw jsonEx;
            }

            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw ex;
            }
        }

        public static Dictionary<string, object> GetElementById(string projectId, string elementId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}/elements/{elementId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            // Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            try
            {
                IRestResponse response = client.Execute(request);
                var elementResponse = JsonConvert.DeserializeObject<ElementResponse>(response.Content);

                if (response.IsSuccessful && elementResponse.Element != null)
                {
                    var element = elementResponse.Element;
                    var elementDict = new Dictionary<string, object>
                        {
                            { "autoIncrementId", element.AutoIncrementId.ToString() },
                            { "completed", element.Completed.ToString() },
                            { "createdAt", element.CreatedAt },
                            { "createdBy", element.CreatedBy },
                            { "id", element.Id },
                            { "name", element.Name },
                            { "starred", element.Starred.ToString() },
                            { "status", element.Status },
                            { "updatedAt", element.UpdatedAt },
                            { "updatedBy", element.UpdatedBy }
                        };

                    var categoryDict = new Dictionary<string, string>
                        {
                            { "id", element.Category.Id },
                            { "name", element.Category.Name }
                        };
                    elementDict["category"] = categoryDict;

                    // Add 'params' nullable field
                    if (element.Params != null)
                    {
                        var paramsDict = new Dictionary<string, object>();
                        foreach (var param in element.Params)
                        {
                            var paramDetails = new Dictionary<string, string>
                                {
                                    { "name", param.Value.Name },
                                    { "parameterGroup", param.Value.ParameterGroup },
                                    { "revitId", param.Value.RevitId },
                                    { "scalar", param.Value.Scalar.ToString() },
                                    { "unit", param.Value.Unit },
                                    { "value", param.Value.Value }
                                };
                            paramsDict[param.Key] = paramDetails;
                        }
                        elementDict["params"] = paramsDict;
                    }

                    // Add 'spatialRelationships' nullable field
                    if (element.SpatialRelationships != null)
                    {
                        var spatialRelationshipsList = new List<Dictionary<string, string>>();
                        foreach (var spatialRelationship in element.SpatialRelationships)
                        {
                            var spatialDict = new Dictionary<string, string>
                            {
                                { "id", spatialRelationship.Id },
                                { "name", spatialRelationship.Name },
                                { "categoryId", spatialRelationship.CategoryId },
                                { "phaseName", spatialRelationship.PhaseName }
                            };
                            spatialRelationshipsList.Add(spatialDict);
                        }
                        elementDict["spatialRelationships"] = spatialRelationshipsList;
                    }

                    // Add 'fields' nullable field
                    if (element.Fields != null)
                    {
                        var fieldsDict = new Dictionary<string, object>();
                        foreach (var field in element.Fields)
                        {
                            var fieldDetails = new Dictionary<string, string>
                                {
                                    { "name", field.Value.Name },
                                    { "type", field.Value.Type },
                                };
                            if (field.Value.Value != null) fieldDetails.Add("value", field.Value.Value.ToString());
                            fieldsDict[field.Key] = fieldDetails;
                        }
                        elementDict["fields"] = fieldsDict;
                    }

                    return elementDict;
                }
            
                else
                {
                    throw new Exception($"API call failed with status: {response.StatusCode}, content: {response.Content}");
                }
            }

            catch (JsonSerializationException jsonEx)
            {
                Console.WriteLine($"JSON Serialization Error: {jsonEx.Message}");
                throw jsonEx;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw ex;
            }
        }
    }
}
