using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_7388
{
    internal class Program
    {
        const double VAT_RATE = 0.01;
        const double ENVIRONMENT_RATE = 0.01;
        const double HOUSEHOLD_PRICE_1 = 5.973;
        const double HOUSEHOLD_PRICE_2 = 7.052;
        const double HOUSEHOLD_PRICE_3 = 8.699;
        const double HOUSEHOLD_PRICE_4 = 15.929;
        const double AGENCY_PRICE = 9.955;
        const double PRODUCTION_PRICE = 11.615;
        const double BUSINESS_PRICE = 22.068;

        //Enter customer name
        static string CustomerName()
        {
            Console.Write("Enter your name: ");
            string customerName = Console.ReadLine();
            return customerName;
        }

        //choose customer type
        static int TypeOfCustomer()
        {

            Console.WriteLine("Type a customer:\n\t1. Household customer " +
                "                               \n\t2. Administrative agency, public services " +
                "                               \n\t3. Production units" +
                "                               \n\t4. Business services");
            Console.Write("Enter your type (1-4): ");
            int type = int.Parse(Console.ReadLine());

            while (type < 1 || type > 4)
            {
                Console.Write("Please enter a number from 1 to 4:");
                type = int.Parse(Console.ReadLine());
            }
            return type;
        }
        //Calculate consumption
        static double AmountOfConsumption()
        {
            double lastWaterMeter, thisWaterMeter;

            while (true)
            {
                Console.Write("Enter last month’s water meter reading: ");
                lastWaterMeter = double.Parse(Console.ReadLine());

                Console.Write("Enter this month’s water meter reading: ");
                thisWaterMeter = double.Parse(Console.ReadLine());

                if (lastWaterMeter >= thisWaterMeter)
                {
                    Console.WriteLine("Error: Last month's reading cannot be greater than this month's reading. Please re-enter the values.");
                }
                else
                {
                    break;
                }
            }

            double consumption = thisWaterMeter - lastWaterMeter;
            return consumption;
        }

        //Calculate prices based on customer type
        static double Price(double consumption, int type)
        {
            
            double price = default;
            
            switch (type)
            {
                case 1: // Household customer with tiered pricing
                    if (consumption <= 10)
                    {
                        price = consumption * HOUSEHOLD_PRICE_1;
                    }
                    else if (consumption <= 20)
                    {
                        price = (10 * HOUSEHOLD_PRICE_1) + ((consumption - 10) * HOUSEHOLD_PRICE_2);
                    }
                    else if (consumption <= 30)
                    {
                        price = (10 * HOUSEHOLD_PRICE_1) + (10 * HOUSEHOLD_PRICE_2) + ((consumption - 20) * HOUSEHOLD_PRICE_3);
                    }
                    else
                    {
                        price = (10 * HOUSEHOLD_PRICE_1) + (10 * HOUSEHOLD_PRICE_2) + (10 * HOUSEHOLD_PRICE_3) + ((consumption - 30) * HOUSEHOLD_PRICE_4);
                    }
                    break;

                case 2: // Administrative agency, public services
                    price = consumption * AGENCY_PRICE;
                    break;

                case 3: // Production units
                    price = consumption * PRODUCTION_PRICE;
                    break;

                case 4: // Business services
                    price = consumption * BUSINESS_PRICE;
                    break;

                default:
                    Console.WriteLine("Invalid customer type.");
                    break;
            }

            return price;
        }
        //Calculate totalBill based on price and VAT
        static double TotalBill(double consumption, int type)
        {
            double price = Price(consumption, type);
            double vatFee = price * VAT_RATE;
            double environmentFee = price * ENVIRONMENT_RATE;
            double totalBill = price + vatFee + environmentFee;
            Console.WriteLine("====================== * ======================\n");

            Console.WriteLine($"Base water bill: {price} VND");
            Console.WriteLine($"VAT (1%): {vatFee} VND");
            Console.WriteLine($"Environment Fee (1%): {environmentFee} VND");
            Console.WriteLine($"Total water bill: {totalBill} VND");

            return totalBill;
        }

        static void Main(string[] args)
        {

            Console.WriteLine("================= WATER BILLING PROGRAM =================\n");

            string name = CustomerName();
            Console.WriteLine($"\nWelcome {name} to the water billing program!!\n");

            int type = TypeOfCustomer();
            double consumption = AmountOfConsumption();
            TotalBill(consumption, type);
            Console.ReadKey();
        }

    }
}
