using System;
using System.Collections.Generic;

// Here, i created a class that i named Order to represent the Order class
public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double total = 0;
        foreach (var product in _products)
        {
            total += product.GetTotalCost();
        }

        // Add shipping cost
        total += _customer.IsInUSA() ? 5 : 35;
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}

// Here, i Create a class that i named Product to represent the Product class and this class holds name, product id, price, and quantity of each product and and rhis class also returns the total cost of product  
public class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    // Getters
    public string GetName() => _name;
    public string GetProductId() => _productId;
    public double GetPrice() => _price;
    public int GetQuantity() => _quantity;

    public double GetTotalCost()
    {
        return _price * _quantity;
    }
}

// Here i created the Customer class which holds the name and addresss and also return them
public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Getters
    public string GetName() => _name;
    public Address GetAddress() => _address;

    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }
}

// Here i created Address class that holds the State, City, Street ad Country of customer and also returns them.
public class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    // Getters
    public string GetStreetAddress() => _streetAddress;
    public string GetCity() => _city;
    public string GetStateProvince() => _stateProvince;
    public string GetCountry() => _country;

    public bool IsInUSA()
    {
        return _country.Equals("Nigeria", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }
}

// This is the main class that runs my program
class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address nigerianAddress = new Address("B3 Rapour close Amakaohia", "Owerri North", "Imo State", "Nigeria");
        Address internationalAddress = new Address("166 Kudirate off Abiola way", "Ikeja Lagos", "Lagos State", "Nigeria");

        // Create customers
        Customer customer1 = new Customer("Mery Akinyemi", nigerianAddress);
        Customer customer2 = new Customer("Emma Odibo", internationalAddress);

        // Create products
        Product product1 = new Product("Zobo", "Z1001", 999.99, 1);
        Product product2 = new Product("Pafe", "P1002", 19.99, 2);
        Product product3 = new Product("Cake", "C1003", 49.99, 1);
        Product product4 = new Product("Pof Pof", "P1004", 199.99, 2);
        Product product5 = new Product("Bones", "B1005", 79.99, 1);

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        // Display order information
        DisplayOrderDetails(order1);
        Console.WriteLine();
        DisplayOrderDetails(order2);
    }

    static void DisplayOrderDetails(Order order)
    {
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine($"Total Price: ${order.CalculateTotalCost():0.00}");
    }
}