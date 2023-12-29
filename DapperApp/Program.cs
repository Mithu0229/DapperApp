// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using DapperApp;

var benchmark = BenchmarkRunner.Run<DatabaseOperations>();

//Console.WriteLine("Hello, World!");
