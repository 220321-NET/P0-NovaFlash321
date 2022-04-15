# Store App 2.0
### Into the Web

## Overview:
The store app is a software that helps customers purchase products from your business. Designed with functionality that would make virtual shopping much simpler!

## Functionality:
* **Add a new customer** - DONE
* **Search customers**
* **View store inventory**
* Display details of an order - DONE
* **Place orders to store locations for customers** - DONE
* View order history of customer - DONE
* View order history of location
* The customer should be able to purchase multiple products - DONE
* Order histories should have the option to be sorted by date (latest to oldest and vice versa) or cost (least expensive to most expensive) - DONE
* The manager should be able to replenish inventory - DONE

## Models:
* Customer
* StoreFront
* Orders
* Product
* Inventory
* LineItems - "Cart" (Individual product item with quantity in an order)
#### Note: add as much models as you would need for your design

## Additional requirements:
* Exception Handling
* Input validation - DONE
* Logging (useful ones)
* **API should be analyzed using Sonar Cloud**
* At least 50% Test Coverage
* Use Moq and Xunit for testing
* **Data should be persisted (no data should be hard coded)**
* **API should be deployed using Azure App Services**
* A CI/CD pipeline should be established use github actions
* DB structure should be 3NF - DONE
* Should have an ER Diagram - DONE
* Code should have xml documentation - GOOD

Tech Stack:
* C#
* Xunit, Moq (for testing)
* Serilog (for logging)
* Azure (for our DB and for deployment)
* Github Actions (pipeline)
* ASP.NET (as our web framework)

## Bonus Features
* Identity Handling (either manually or using ASP.NET Identity)
* Web Frontend (either using CSS/HTML/JS or any other web frontend framework)
* Containerize your backend
* Email
* and anything else you can think of!