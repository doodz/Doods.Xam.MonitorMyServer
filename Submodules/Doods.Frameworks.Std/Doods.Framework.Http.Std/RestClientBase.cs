using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std.Authentication;
using Doods.Framework.Http.Std.Extensions;
using Doods.Framework.Http.Std.Serializers;
using RestSharp;

namespace Doods.Framework.Http.Std
{
    public interface IHttpClient
    {
        Task<IRestResponse> ExecuteAsync(IRestRequest request);
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request);
    }

    public abstract class RestClientBase : RestClient, IHttpClient
    {
        //TODO logger
        //private readonly ILog _logger =
        //    LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly IConnection Connection;

        public RestClientBase(IConnection connection) : this(connection, new NewtonsoftJsonSerializer())
        {
        }

        public RestClientBase(IConnection connection, NewtonsoftJsonSerializer serializer)
        {
            var baseUrl = new Uri(connection.Host);
            BaseUrl = baseUrl.SetPort(connection.Port);

            Connection = connection;

            AddHandler("application/json", () => serializer);
            AddHandler("text/json", () => serializer);
            AddHandler("text/plain", () => serializer);
            AddHandler("text/x-json", () => serializer);
            AddHandler("text/javascript", () => serializer);
            AddHandler("*+json", () => serializer);

            var auth = new Authenticator(connection.Credentials);
            Authenticator = auth.CreatedAuthenticator;
            FollowRedirects = false;
            CookieContainer = new CookieContainer();
        }

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            //_logger.Info($"Calling ExecuteTaskAsync. BaseUrl: {BaseUrl} Resource: {request.Resource} Parameters: {string.Join(", ", request.Parameters)}");
            AddHeaders(request);
            var response = await base.ExecuteAsync(request).ConfigureAwait(false);
            response = await CheckRedirectsHeaderAsync(response, request).ConfigureAwait(false);
            CheckResponseStatusCode(response);
            return response;
        }

        public async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request)
        {
            //_logger.Info($"Calling ExecuteTaskAsync. BaseUrl: {BaseUrl} Resource: {request.Resource} Parameters: {string.Join(", ", request.Parameters)}");
            AddHeaders(request);
            var response = await base.ExecuteAsync<T>(request).ConfigureAwait(false);
            response = await CheckRedirectsHeaderAsync(response, request).ConfigureAwait(false);
            CheckResponseStatusCode(response);
            return response;
        }

        protected abstract void AddHeaders(IRestRequest request);

        protected abstract string DeserializeError(IRestResponse response);

        private void CheckResponseStatusCode(IRestResponse response)
        {
            

            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new AuthorizationException(response.Content);

            if (response.StatusCode == HttpStatusCode.Forbidden) throw new ForbiddenException(response.ErrorMessage);

            if ((int) response.StatusCode >= 400)
            {
                var errorMessage = response.ErrorMessage;
                var friendly = false;
                if (response.Content != null)
                    try
                    {
                        errorMessage = DeserializeError(response);
                        friendly = true;
                    }
                    catch
                    {
                    }

                //_logger.Error($"Error in request: {response.Content}");

                throw new RequestFailedException(errorMessage, friendly);
            }
            if (response.ErrorException != null)
                throw response.ErrorException;
        }


        private async Task<IRestResponse> CheckRedirectsHeaderAsync(IRestResponse response,
            IRestRequest request)
        {
            var responsenew = await RedirectIfNeededAndGetResponseAsync(response, request).ConfigureAwait(false);
            return responsenew = await RedirectIfMovedPermanentlyAndGetResponseAsync(response, request)
                .ConfigureAwait(false);
        }

        private async Task<IRestResponse<T>> CheckRedirectsHeaderAsync<T>(IRestResponse<T> response,
            IRestRequest request)
        {
            var responsenew = await RedirectIfNeededAndGetResponseAsync(response, request).ConfigureAwait(false);
            return responsenew = await RedirectIfMovedPermanentlyAndGetResponseAsync(response, request)
                .ConfigureAwait(false);
        }

        private async Task<IRestResponse<T>> RedirectIfMovedPermanentlyAndGetResponseAsync<T>(IRestResponse<T> response,
            IRestRequest request)
        {
            while (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                var newLocation = GetNewLocationFromHeader(response);

                if (newLocation == null)
                    return response;

                request.Resource = RemoveBaseUrl(newLocation);
                response = await base.ExecuteAsync<T>(request).ConfigureAwait(false);
            }

            return response;
        }


        private async Task<IRestResponse> RedirectIfMovedPermanentlyAndGetResponseAsync(IRestResponse response,
            IRestRequest request)
        {
            while (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                var newLocation = GetNewLocationFromHeader(response);

                if (newLocation == null)
                    return response;

                request.Resource = RemoveBaseUrl(newLocation);
                response = await base.ExecuteAsync(request).ConfigureAwait(false);
            }

            return response;
        }


        private async Task<IRestResponse<T>> RedirectIfNeededAndGetResponseAsync<T>(IRestResponse<T> response,
            IRestRequest request)
        {
            while (response.StatusCode == HttpStatusCode.Redirect)
            {
                var newLocation = GetNewLocationFromHeader(response);

                if (newLocation == null)
                    return response;

                request.Resource = RemoveBaseUrl(newLocation);
                response = await base.ExecuteAsync<T>(request).ConfigureAwait(false);
            }

            return response;
        }


        private async Task<IRestResponse> RedirectIfNeededAndGetResponseAsync(IRestResponse response,
            IRestRequest request)
        {
            while (response.StatusCode == HttpStatusCode.Redirect)
            {
                var newLocation = GetNewLocationFromHeader(response);

                if (newLocation == null)
                    return response;

                request.Resource = RemoveBaseUrl(newLocation);
                response = await base.ExecuteAsync(request).ConfigureAwait(false);
            }

            return response;
        }


        private static Parameter GetNewLocationFromHeader(IRestResponse response)
        {
            return response.Headers
                .FirstOrDefault(x => x.Type == ParameterType.HttpHeader &&
                                     x.Name.Equals(HttpResponseHeader.Location.ToString(),
                                         StringComparison.InvariantCultureIgnoreCase));
        }

        private string RemoveBaseUrl(Parameter newLocation)
        {
            return newLocation.Value.ToString().Replace(Connection.Host, string.Empty);
        }
    }
}