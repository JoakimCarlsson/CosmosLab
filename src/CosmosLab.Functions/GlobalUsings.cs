﻿global using System.Net;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using Bogus;

global using CosmosLab.Functions.Extensions;
global using CosmosLab.EntityFramework.Extensions;
global using CosmosLab.EntityFramework.Services;
global using CosmosLab.EntityFramework.Persistance;
global using CosmosLab.CosmosSDK.Extensions;
global using CosmosLab.Shared.Models;

global using Azure.Core.Serialization;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Azure.Cosmos;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Azure.Functions.Worker;
global using Microsoft.Azure.Functions.Worker.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Hosting;
