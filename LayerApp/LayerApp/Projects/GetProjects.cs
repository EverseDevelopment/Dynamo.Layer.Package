﻿using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Projects
{
    public class GetProjects
    {
        // Empty constructor to avoid import in Dynamo
        private GetProjects() { }

        public static List<Dictionary<string, string>> GetAllProjects(string bearerToken)
        {
            var client = new RestClient("https://api.layer.team/projects");
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
                    var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
                    var projectsDictList = new List<Dictionary<string, string>>();

                    foreach (var project in projectResponse.Projects)
                    {
                        var projectDict = new Dictionary<string, string>
                        {
                            { "createdAt", project.CreatedAt.ToString() },
                            { "createdBy", project.CreatedBy },
                            { "company", project.Company },
                            { "name", project.Name },
                            { "location", project.Location },
                            { "id", project.Id },
                            { "status", project.Status },
                            { "type", project.Type }
                        };
                        projectsDictList.Add(projectDict);
                    }

                    return projectsDictList;
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

        public static Dictionary<string, string> GetProjectById(string projectId, string bearerToken)
        {
            var client = new RestClient($"https://api.layer.team/projects/{projectId}");
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
                    var projectResponse = JsonConvert.DeserializeObject<SpecificProjectResponse>(response.Content);
                    var project = projectResponse.Project;

                    var projectDict = new Dictionary<string, string>
                    {
                        { "createdAt", project.CreatedAt.ToString() },
                        { "createdBy", project.CreatedBy },
                        { "company", project.Company },
                        { "name", project.Name },
                        { "location", project.Location },
                        { "id", project.Id },
                        { "status", project.Status },
                        { "type", project.Type },
                        { "role", projectResponse.Role } // role is outside the project object
                    };

                    return projectDict;
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
