# Output Caching in .NET Core Web API using Minimal API Architecture

Caching is an essential technique to improve the performance and responsiveness of web applications by reducing the load on databases or external services. In .NET Core Web API, output caching allows developers to store the response output and serve it directly from the cache for subsequent requests, reducing the need to recompute the response.

In this guide, we will explore how to implement output caching in a .NET Core Web API project using the minimal API architecture.

## Prerequisites
- .NET SDK (6.0 or later)

## Testing the API
Access the /current-time endpoint multiple times within 10 seconds and observe the response. The date and time should remain the same due to caching.

## Conclusion
Output caching is a powerful feature in .NET Core Web API that can significantly improve the performance of your application. By leveraging the minimal API architecture, developers can quickly and efficiently implement caching mechanisms in their projects.