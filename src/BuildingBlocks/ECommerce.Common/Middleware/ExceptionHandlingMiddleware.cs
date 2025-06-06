﻿using ECommerce.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException ex)
            {
                // Only log at debug level for cancellations as they are often expected
                _logger.LogDebug($"Request was canceled: {ex.Message}");

                // Check if the client has disconnected before trying to write a response
                if (!context.RequestAborted.IsCancellationRequested)
                {
                    await HandleCancellationAsync(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Bir hata meydana geldi: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleCancellationAsync(HttpContext context)
        {
            // Only process if response hasn't started yet
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest; // Non-standard status code for client closed request

                var exceptionDetails = new ExceptionDetails
                {
                    StatusCode = StatusCodes.Status499ClientClosedRequest,
                    Message = "Request was canceled"
                };

                var json = JsonSerializer.Serialize(exceptionDetails);
                await context.Response.WriteAsync(json);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Only process if response hasn't started yet
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("Response has already started, cannot write error response");
                return;
            }

            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                Exceptions.ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var exceptionDetails = new ExceptionDetails
            {
                StatusCode = statusCode,
                Message = ex.Message
            };

            if (ex is Exceptions.ValidationException validationException)
            {
                exceptionDetails.Details = JsonSerializer.Serialize(validationException.Errors);
            }

            var json = JsonSerializer.Serialize(exceptionDetails);
            await context.Response.WriteAsync(json);
        }
    }
}
