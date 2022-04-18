using Serilog;
using Microsoft.AspNetCore.Builder;

Console.WriteLine("Hello, MatchMake.Backend!");

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Hangfire
// https://www.youtube.com/watch?v=iilRdmNILC8
// https://github.com/codaza/Hangfire