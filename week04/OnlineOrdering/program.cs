using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address(
            "123 Main Street",
            "Boise",
            "Idaho",
            "USA");

        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);

        order1.AddProduct(new Product("Laptop", "P101", 850.00, 1));
        order1.AddProduct(new Product("Mouse", "P102", 25.00, 2));
        order1.AddProduct(new Product("Keyboard", "P103", 45.00, 1));

        Address address2 = new Address(
            "45 King Street",
            "Toronto",
            "Ontario",
            "Canada");

        Customer customer2 = new Customer("Emily Johnson", address2);

        Order order2 = new Order(customer2);

        order2.AddProduct(new Product("Monitor", "P201", 220.00, 2));
        order2.AddProduct(new Product("Webcam", "P202", 60.00, 1));
        order2.AddProduct(new Product("Headset", "P203", 80.00, 1));

        Console.WriteLine("ORDER 1");
        Console.WriteLine("--------------------------");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());

        Console.WriteLine($"\nTotal Price: ${order1.GetTotalPrice()}");

        Console.WriteLine("\n=============================\n");

        Console.WriteLine("ORDER 2");
        Console.WriteLine("--------------------------");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());

        Console.WriteLine($"\nTotal Price: ${order2.GetTotalPrice()}");
    }
}