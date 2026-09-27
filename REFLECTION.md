# Copilot Reflection

## How Copilot Assisted

GitHub Copilot helped connect the Blazor WebAssembly client to the ASP.NET Core API. It configured the client `HttpClient` with the API base address and timeout, enabled CORS in the API, and added a Products navigation link. Copilot also helped implement loading, timeout, HTTP-error, and invalid-JSON states in the product component, along with structured logger messages for browser-console debugging.

For the API response, Copilot replaced anonymous product objects with explicit `Product` and `Category` records. This made the JSON contract clear and allowed each product response to include a nested category object. On the client, matching `Product` and `Category` models were added so the Razor table could display the category name.

Copilot also suggested two caching layers to reduce unnecessary work. The Blazor client keeps a successful product response in a scoped in-memory cache, avoiding another API request when a user returns to the Products page during the same browser session. The API uses ASP.NET Core output caching for the product-list endpoint for 60 seconds, and the fixed product data is created once at application startup rather than per cache miss.

## Challenges and Resolutions

A key challenge was that the client originally used a relative API path, which pointed to the Blazor application's address rather than the separate API project. Copilot traced the configured ports, set the client base address, and added the CORS policy needed for cross-origin requests.

Another issue was an endpoint-name mismatch: the client requested `products` while the API exposed `productlist`. The exception logging added to the component made the HTTP failure visible in the browser console, and the client endpoint constant was aligned with the API route.

During implementation, build validation caught a missing `System.Text.Json` namespace for `JsonException` and a syntax mistake while restructuring the minimal API endpoint. Copilot used the compiler feedback to make focused corrections and rebuilt the solution after each change. A port conflict also occurred when starting a second API instance; using a temporary port made it possible to verify the API response without interrupting the existing process.

## Lessons Learned

This project showed that Copilot is most effective when it is given a concrete starting point, such as a component, endpoint, compiler error, or expected JSON response. Reviewing the generated code remains important: the API route, development ports, CORS origins, and cache lifetime all needed to match this application's setup.

I also learned to use Copilot iteratively. Making a small change, building it immediately, and responding to exact compiler or runtime feedback kept integration issues localized. Copilot was useful for proposing patterns such as typed models, structured logging, and built-in caching, while the final decisions still depended on the application's requirements and verification results.
