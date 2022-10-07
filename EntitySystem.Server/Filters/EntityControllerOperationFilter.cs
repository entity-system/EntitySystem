using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EntitySystem.Server.Controllers;
using EntitySystem.Server.Exceptions;
using EntitySystem.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EntitySystem.Server.Filters;

public class EntityControllerOperationFilter : IOperationFilter
{
    private readonly Regex _entityIdRegex = new("/[^/]+/\\{id\\}$");

    private readonly Regex _entityRegex = new("/[^/]+$");

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor is not ControllerActionDescriptor controllerDescriptor) return;

        var entityControllerType = controllerDescriptor.ControllerTypeInfo.BaseType;

        while (entityControllerType != null && (entityControllerType is not {IsGenericType: true} || entityControllerType.GetGenericTypeDefinition() != typeof(EntityController<,>)))
            entityControllerType = entityControllerType.BaseType;

        if (entityControllerType == null) return;

        var type = entityControllerType.GetGenericArguments()[1];

        switch (context.ApiDescription.HttpMethod)
        {
            case "GET" when _entityIdRegex.Match(context.ApiDescription.RelativePath ?? throw new GeneralFriendlyException("Invalid operation filter path.")).Success:
            case "POST" when _entityRegex.Match(context.ApiDescription.RelativePath ?? throw new GeneralFriendlyException("Invalid operation filter path.")).Success:
            {
                AddResponseType(type);

                break;
            }
            case "GET" when _entityRegex.Match(context.ApiDescription.RelativePath).Success:
            {
                var listedType = typeof(List<>).MakeGenericType(type);

                AddResponseType(listedType);

                break;
            }
        }

        void AddResponseType(Type responseType = null)
        {
            var result = context.SchemaRepository.TryLookupByType(responseType, out var schema) ? schema : context.SchemaGenerator.GenerateSchema(responseType, context.SchemaRepository);

            if (!operation.Responses.TryGetValue("200", out var response)) response = operation.Responses["200"] = new OpenApiResponse {Description = "Success"};

            if (responseType != null) response.Content["application/json"] = new OpenApiMediaType {Schema = result};
        }
    }
}