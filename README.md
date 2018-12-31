CsBenchmark
==============
version: 0.1.0

### .NET Benchmarking/Performance Utility

Tools for setting up, running, and printing .NET benchmark results. 

Includes performance loggers to help track benchmark loops and steps.

A TaskRunner which takes a unit of work and measures its runtime repeatedly.

TimeUnit enum like class and helpers for working converting between TimeUnits and formatting to user readable output.

ResultSets, StepResultSet, and StepDescriptor used to keep track of benchmark information and results with ToString() and ToJson() methods to print human or machine readable benchmark results.

Check out the `Examples/` to see some examples of how to use the tool.
