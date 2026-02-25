# Optima-Test SupermarketCheckout Lambda

This project implements a serverless supermarket checkout system using AWS Lambda, DynamoDB and .NET 10.0.

## Project Structure

- `src/SupermarketCheckout.Lambda/` — Main Lambda function code
  - `Function.cs` — Lambda entry point
  - `Startup.cs` — Dependency injection setup
  - `Interfaces/` — Interfaces for checkout and storage providers
  - `Models/` — Data models for requests, responses, and pricing rules
  - `Services/` — Business logic and storage providers
- `tests/SupermarketCheckout.Tests/` — Unit tests for the Lambda function and services
- `template.yaml` — AWS SAM template for deployment
- `.gitignore` — Git ignore rules
- `samconfig.toml` — SAM CLI configuration

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- AWS CLI
- AWS SAM CLI

### Build

```
sam build
```

### Test

```
dotnet test tests/SupermarketCheckout.Tests/SupermarketCheckout.Tests.csproj
```

### Deploy

```
sam deploy --guided # First time
same deply # After saving config
```

## Usage

Send a scan request to the deployed Lambda endpoint.

## Input and Output Payloads

### Input: CheckoutRequest
The Lambda expects a JSON payload with the following structure:

```
{
  "SessionId": "string (optional - guid recommended and generated if not supplied)",
  "Items": ["item1", "item2", ...]
}
```
- `SessionId`: Optional string (guid best) to identify the checkout session.
- `Items`: Array of item SKUs or names to be added to the 'basket'.

### Output: CheckoutResponse
The Lambda returns a JSON response with the following structure:

```
{
  "SessionId": "string",
  "TotalPrice": integer,
  "ItemBreakdown": {
    "item1": integer,
    "item2": integer
  },
  "Error": "string (optional)",
  "Success": boolean
}
```
- `SessionId`: The session identifier.
- `TotalPrice`: The total price for all items.
- `ItemBreakdown`: Dictionary mapping item names/SKUs to their total quantity.
- `Error`: Optional error message if the scan failed.
- `Success`: Boolean indicating if the scan was successful (true if no error).

### Future considerations
- Usual production hardening such as Authentication, Rate Limiting, etc.
- Allow Pricing Rules to be persisted to DynamoDB and loaded dynamically to allow them to be modified without re-deploying
- Add some DynamoDB Integration tests
- Improve resilience by allowing erroneous SKUs to be returned whilst still adding valid SKUs to the basket
- Improve monitoring of performance and actions, including reporting erroneous SKUs etc.
- Store an updated Basket total and other necessary metadata in a separate table
- Add a GET endpoint for returning the Basket without adding SKUs