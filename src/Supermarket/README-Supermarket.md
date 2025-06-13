# Supermarket

# Overview
A 'CheckoutService' will scan items and add them to the 'CartService'. When all items have been scanned, the 'CheckoutService' will call
GetTotalPrice(). GetTotalPrice() works by passing the contents of CartService to the PricingService. The PricingService is already initialised with 
a table of the unit prices and special price (if applicable). The pricing service will calculate the prices and pass the answer back out to the CheckoutService.

# Assumptions
* Handles only a single customer purchasing items at a time. As the basket of goods is scanned there is a single tally in the system
of those goods.

* Special pricing uses modulus and remainder operators to calculate different prices. Assuming the customer buys 5 of 'A' the price will 
be 130 (total of 3) + 100 (remaining 2 for 50) for a total price of 230.

* I am using the following SKU prices:

| SKU | Unit Price | Special Price |
|-----|-----------:|---------------|
| A   | 50     	   | 3 for 130     |
| B   | 30     	   | 2 for 45      |
| C   | 20     	   |               |
| D   | 15     	   |               |


The API project is just a thin veneer. The services are where the business logic is kept and the main testing target.

* There is no async work here because there are no blocking calls.

* The CartService and PricingService are singletons so the software paradigm matches the real world where prices and what is in a cart
are persistent.

* CheckoutService has a scoped lifetime as it doesn't need to be a singleton.

* Invalid sku codes are ignored as they are scanned

# Improvements if I had more time
* Make it thread safe.