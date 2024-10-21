namespace Bank_uppgift_nmr2;
   
    using System;

class BankAccount
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Insättning lyckades. Nytt saldo: {Balance}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"Uttag lyckades. Nytt saldo: {Balance}");
        }
        else
        {
            Console.WriteLine("Otillräckligt saldo.");
        }
    }

    public void TransferTo(BankAccount targetAccount, decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            targetAccount.Balance += amount;
            Console.WriteLine($"Överföring på {amount} till konto {targetAccount.AccountNumber} lyckades.");
        }
        else
        {
            Console.WriteLine("Otillräckligt saldo.");
        }
    }
}

class Program
{
    static BankAccount personalAccount = new BankAccount("001", 1000m);
    static BankAccount savingsAccount = new BankAccount("002", 5000m);
    static BankAccount investmentAccount = new BankAccount("003", 10000m);

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Sätt in\n2. Ta ut\n3. Överför\n4. Avsluta");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": HandleTransaction("deposit"); break;
                case "2": HandleTransaction("withdraw"); break;
                case "3": HandleTransfer(); break;
                case "4": running = false; break;
                default: Console.WriteLine("Ogiltigt val."); break;
            }
        }
    }

    static void HandleTransaction(string type)
    {
        Console.Write("Ange kontonummer (001, 002, 003): ");
        BankAccount account = GetAccount(Console.ReadLine());
        Console.Write("Ange belopp: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        if (type == "deposit") account.Deposit(amount);
        else account.Withdraw(amount);
    }

    static void HandleTransfer()
    {
        Console.Write("Från-kontonummer: ");
        BankAccount fromAccount = GetAccount(Console.ReadLine());
        Console.Write("Till-kontonummer: ");
        BankAccount toAccount = GetAccount(Console.ReadLine());
        Console.Write("Ange belopp: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        fromAccount.TransferTo(toAccount, amount);
    }

    static BankAccount GetAccount(string accountNumber)
    {
        return accountNumber switch
        {
            "001" => personalAccount,
            "002" => savingsAccount,
            "003" => investmentAccount,
            _ => null,
        };
    }
}


