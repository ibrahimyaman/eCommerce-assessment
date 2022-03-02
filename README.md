# eCommerce-assessment

This work is a simple e-commerce API to manage product, cart and order transactions.

### Installation
First thing after you download source codes is to write connection string of your MsSql server in **eCommerce.DataAccess/dalsettings.json**. And then migrate database structure to the server by using *update-database*.

`{
  "ConnectionStrings": {
    "Default": "conn"
  }
}`
