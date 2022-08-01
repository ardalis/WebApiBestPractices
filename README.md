# ASP.NET Core 6 Web Api Best Practices

Resources related to my Pluralsight course.

[Watch the course - ASP.NET Core 6 Web API: Best Practices](https://www.pluralsight.com/courses/aspdotnet-core-6-web-api-best-practices)

## References

Below are a selection of links I referenced when building this course.

### RFCs

- [RFC2616 - HTTP 1.1](https://datatracker.ietf.org/doc/html/rfc2616#section-10)
- [RFC6454 - The Web Origin Concept](https://datatracker.ietf.org/doc/html/rfc6454)
- [RFC6585 - Additional HTTP Status Codes](https://www.rfc-editor.org/rfc/rfc6585#section-4)
- [RFC6648 - Deprecating 'X-' Prefix](https://datatracker.ietf.org/doc/html/rfc6648)
- [RFC6797 - HTTP Strict Transport Security](https://datatracker.ietf.org/doc/html/rfc6797)
- [RFC6819 - Authorization Headers](https://www.rfc-editor.org/rfc/rfc6819#section-5.4.1)
- [RFC7231 - HTTP 1.1 - Semantics and Content](https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.2)
- [RFC7234 - HTTP 1.1 - Caching](https://datatracker.ietf.org/doc/html/rfc7234)
- [RFC7396 - JSON Merge Patch](https://datatracker.ietf.org/doc/html/rfc7396)
- [RFC7807 - Problem Details for HTTP APIs](https://datatracker.ietf.org/doc/html/rfc7807)

### ASP.NET Core Docs

- [Create Web APIs with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0)
- [JsonPatch in ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-6.0)
- [Minimal APIs Overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
- [Response Caching Middleware in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-6.0)
- [Response Compression in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-6.0)
- [Web APIs - Handle Errors](https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0)
- [Enable CORS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0)
- [Enforce HTTPS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
- [Overview of ASP.NET Core Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-6.0)
- [Resource-based authorization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/resourcebased?view=aspnetcore-6.0)
- [Globalization and localization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-6.0)
- [Use scoped services within a BackgroundService](https://docs.microsoft.com/en-us/dotnet/core/extensions/scoped-service)
- [Response Caching Middleware in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-6.0)

### Stack Overflow Discussions

- [Create request with POST, which response codes 200 or 201 and content](https://stackoverflow.com/questions/1860645/create-request-with-post-which-response-codes-200-or-201-and-content)
- [Should a RESTful PUT Operation Return Something](https://stackoverflow.com/questions/797834/should-a-restful-put-operation-return-something)
- [403 Forbidden vs 401 Unauthorized Http Responses](https://stackoverflow.com/questions/3297048/403-forbidden-vs-401-unauthorized-http-responses)
- [How to use Created or CreatedAtAction or CreatedAtRoute in ASP.NET Core API](https://stackoverflow.com/questions/47939945/how-to-use-created-or-createdataction-createdatroute-in-an-asp-net-core-api)
- [What are the main differences between JWT and OAuth Authentication](https://stackoverflow.com/questions/39909419/what-are-the-main-differences-between-jwt-and-oauth-authentication)
- [Pagination Response Payload from a RESTful API](https://stackoverflow.com/questions/12168624/pagination-response-payload-from-a-restful-api)
- [Swagger unexpected API PATCH action documentation of JsonPatchDocument in example request body](https://stackoverflow.com/questions/65599406/swagger-unexpected-api-patch-action-documentation-of-jsonpatchdocument-in-exampl)
- [What should response of RESTful API for Patch HTTP Method?](https://stackoverflow.com/questions/54666244/what-should-response-of-rest-full-api-for-patch-http-method)
- [Verifying JWT Signed with the RS 256 Algorithm using public key in C#](https://stackoverflow.com/questions/34403823/verifying-jwt-signed-with-the-rs256-algorithm-using-public-key-in-c-sharp)
- [What is the meaning of ValidateIssuer and ValidateAudience in JWT?](https://stackoverflow.com/questions/70597009/what-is-the-meaning-of-validateissuer-and-validateaudience-in-jwt)
- [Autofac Propert Injection in Base Class](https://stackoverflow.com/questions/26808072/autofac-property-injection-in-base-class)

### Software Engineering StackExchange Discussions

- [Should I return an HTTP 400 Bad Request Status if a parameter is syntactically correct](https://softwareengineering.stackexchange.com/questions/329229/should-i-return-an-http-400-bad-request-status-if-a-parameter-is-syntactically/342896#342896)
- [To Include a Resource ID in the payload or to derive from URI](https://softwareengineering.stackexchange.com/questions/263925/to-include-a-resource-id-in-the-payload-or-to-derive-from-uri)
- [Which HTTP Verb Should I Use to Trigger an Action in a REST Web Service](https://softwareengineering.stackexchange.com/questions/261552/which-http-verb-should-i-use-to-trigger-an-action-in-a-rest-web-service)

### Various Other Resources

- [Meet Hyrum and Postel](https://nordicapis.com/meet-hyrum-and-postel/)
- [Hyrum's Law](https://www.hyrumslaw.com/)
- [Robustness Principle](https://en.wikipedia.org/wiki/Robustness_principle)
- [REST API Design Tutorial with Example](https://restfulapi.net/rest-api-design-tutorial-with-example/)
- [3 Ways to Return Data from the Controllers Action Method in ASP.NET Core](https://www.c-sharpcorner.com/article/3-ways-to-return-the-data-from-controller-action-method-in-asp-net-core/)
- [ASP.NET Core Web API Creating and Validating JWT Json Web Token](https://www.c-sharpcorner.com/article/asp-net-core-web-api-creating-and-validating-jwt-json-web-token/)
- [Using ProducesResponseType to Write a Better Web API Action in .NET Core](https://medium.com/@mohammed0hamdan/using-producesresponsetype-to-write-a-better-web-api-actions-in-net-core-18e080c9bf00)
- [Response Compression Middleware in ASP.NET Core](https://www.c-sharpcorner.com/article/response-compression-middleware-in-asp-net-core/)
- [PUT vs POST](https://www.keycdn.com/support/put-vs-post)
- [GitHub API: Pages](https://docs.github.com/en/rest/pages)
- [GitHub API: Traversing with Pagination](https://docs.github.com/en/rest/guides/traversing-with-pagination)
- [JWT Debugger](https://jwt.io/)
- [Anatomy of a JWT](https://fusionauth.io/learn/expert-advice/tokens/anatomy-of-jwt)
- [JWT Security](https://owasp.org/www-chapter-belgium/assets/2021/2021-02-18/JWT-Security.pdf)
- [Getting API Security Right](https://pragmaticwebsecurity.com/files/talks/gettingAPIsecurityright.pdf)
- [JSON Web Tokens Security Cheat Sheet](https://pragmaticwebsecurity.com/files/cheatsheets/jwt.pdf)
- [Secure .NET Microservices](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/secure-net-microservices-web-applications/#authenticate-with-bearer-tokens)
- [Implementing JWT Authentication in ASP.NET Core 5](https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5)
- [IANA Link Relations](https://www.iana.org/assignments/link-relations/link-relations.xml)
- [Content Negotiation in Web API](https://code-maze.com/content-negotiation-web-api/)
- [5 Ways to Set the URLs for an ASP.NET Core App](https://andrewlock.net/5-ways-to-set-the-urls-for-an-aspnetcore-app/)
- [Enforce HTTPS Correctly in ASP.NET Core APIs](https://recaffeinate.co/post/enforce-https-aspnetcore-api/)
- [Simplify your ASP.NET Core API models with C# Records](https://www.daveabrock.com/2020/11/18/simplify-api-models-with-records/)
- [JSON Patch](https://jsonpatch.com/)
- [How to use JSON Patch in .NET Core](https://garywoodfine.com/how-to-use-jsonpatch-in-net-core/)
- [JSON Patch Support with Swagger and ASP.NET Core 3.1](https://michael-mckenna.com/swagger-with-asp-net-core-3-1-json-patch/)
- [ASP.NET Core Web API - Post, Put, Delete](https://code-maze.com/net-core-web-development-part6/)
- [OWASP JSON Web Token Cheat Sheet for Java](https://cheatsheetseries.owasp.org/cheatsheets/JSON_Web_Token_for_Java_Cheat_Sheet.html)
- [Hacking JWT Tokens: Brute Forcing Weak Signing Key JWT Cracker](https://blog.pentesteracademy.com/hacking-jwt-tokens-bruteforcing-weak-signing-key-jwt-cracker-5d49d34c44)
- [JWT Authentication with Asymmetric Encryption using certificates in ASP.NET Core](https://dev.to/eduardstefanescu/jwt-authentication-with-asymmetric-encryption-using-certificates-in-asp-net-core-2o7e)
- [The Hard Parts of JWT Security Nobody Talks About](https://www.pingidentity.com/en/resources/blog/post/jwt-security-nobody-talks-about.html)
- [Six Ways to Organize Minimal APIs in ASP.NET Core Applications](http://www.binaryintellect.net/articles/f3dcbb45-fa8b-4e12-b284-f0cd2e5b2dcf.aspx)
- [From MVC to Minimal APIs with ASP.NET Core 6.0](https://benfoster.io/blog/mvc-to-minimal-apis-aspnet-6/)
- [Writing decoupled code with MediatR: The Mediator Pattern](https://samwalpole.com/writing-decoupled-code-with-mediatr-the-mediator-pattern)
- [Middleware for Response Caching in .NET Core Web APIs](https://thecodeblogger.com/2021/06/06/middleware-for-response-caching-in-net-core-web-apis/)
- [Response Caching in ASP.NET Core](https://code-maze.com/aspnetcore-response-caching/)

### GitHub Repos

- [REST API Response Formats](https://github.com/cryptlex/rest-api-response-format)
- [StructuredMinimalAPI](https://github.com/michelcedric/StructuredMinimalApi)
- [FastEndpoints](https://github.com/dj-nitehawk/FastEndpoints)
- [Minimal API Playground](https://github.com/DamianEdwards/MinimalApiPlayground)
- [Swashbuckle.WebApi](https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/1230)
- [JWT-API Sample](https://github.com/evgomes/jwt-api)
- [JWT_Tool Wiki](https://github.com/ticarpi/jwt_tool/wiki#best-practice)
- [jwt-cracker](https://github.com/lmammino/jwt-cracker)
- [JwtAuthentication](https://github.com/StefanescuEduard/JwtAuthentication)
- [IdentityServer BFF Samples](https://github.com/DuendeSoftware/Samples/tree/main/IdentityServer/v5/BFF/JsBffSample)
