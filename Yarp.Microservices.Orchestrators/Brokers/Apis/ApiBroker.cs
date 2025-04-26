// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using RESTFulSense.Clients;



namespace Yarp.Microservices.Orchestrators.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly HttpClient httpClient;

        public ApiBroker(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl)
        {
            return await this.apiClient.GetContentAsync<T>(relativeUrl);
        }

        private async ValueTask<T> GetWithoutAuthorizationHeaderAsync<T>(string relativeUrl)
        {
            return await this.apiClient.GetContentAsync<T>(relativeUrl);
        }

        private async ValueTask<byte[]> GetByteArrayAsync(string relativeUrl)
        {
            return await this.apiClient.GetContentByteArrayAsync(relativeUrl);
        }

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content)
        {
            return await this.apiClient.PostContentAsync<T>(relativeUrl, content);
        }

        private async ValueTask<TResult> PostAsync<TRequest, TResult>(string relativeUrl, TRequest content)
        {
            return await this.apiClient.PostContentAsync<TRequest, TResult>(
                relativeUrl,
                content,
                mediaType: "application/json",
                ignoreDefaultValues: true);
        }


        private async ValueTask<TResult> PostFormAsync<TRequest, TResult>(string relativeUrl, TRequest content)
            where TRequest : class
        {
            return await this.apiClient.PostFormAsync<TRequest, TResult>(
                relativeUrl,
                content);
        }


        private async ValueTask<TResult> PutAsync<TRequest, TResult>(string relativeUrl, TRequest content)
        {
            return await this.apiClient.PutContentAsync<TRequest, TResult>(
                relativeUrl,
                content,
                mediaType: "application/json",
                ignoreDefaultValues: true);
        }

        private async ValueTask<T> PutAsync<T>(string relativeUrl, T content)
        {
            return await this.apiClient.PutContentAsync<T>(relativeUrl, content);
        }

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl)
        {
            return await this.apiClient.DeleteContentAsync<T>(relativeUrl);
        }

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
          

            string apiBaseUrl = "https://localhost:7118/";
            this.httpClient.BaseAddress = new Uri(apiBaseUrl);
            return new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}