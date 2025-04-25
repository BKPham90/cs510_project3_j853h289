module Project3.ErrorHandler

open System

open Microsoft.Extensions.Logging

open Giraffe

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message