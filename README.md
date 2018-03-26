# Drupal API Mapping

## What is this repo for?

This is just an example on how to connect to the Drupal REST API and parse the JSON response message.

In this repository I provide also a way to map the message to a domain model of the demo application, using Automapper (https://github.com/AutoMapper/AutoMapper).

## Demo application context
The demo app retrieves the list Catalogue and their Products from Drupal.

Products are intended to be fuits or vegetables, for this reason, each of them is available in one or more Seasons.

The data strucure that the Drupal API returns may look quite unusual. However it achieves the goal to demonstrate how a mapping can be implemented.

## Setup
- clone the repo
- go to the `src` folder
- `dotnet restore`
- switch to the `CatalogueProducts.Tests` folder
- `dotnet test`
- switch to the `CatalogueProducts` folder
- `dotnet run`