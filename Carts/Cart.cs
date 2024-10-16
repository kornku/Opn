namespace Opn.Carts;

public class Cart : IDisposable
{
    public List<Product> Products { get; set; } = new List<Product>();
    public Dictionary<string, Discount> Discounts { get; set; } = new Dictionary<string, Discount>();

    public static Cart CreateCart()
    {
        return new Cart();
    }

    public void SetProduct(List<Product> products)
    {
        this.Products.AddRange(products);
    }

    public bool HasFreebie()
    {
        return Products.Any(x => x.Id == 1);
    }

    public double CalculateAmount(bool checkFreebie, bool checkDiscount)
    {
        double amount = this.Products.Sum(x => x.Total * x.Amount);
        if (checkFreebie && HasFreebie())
        {
            amount -= this.Products.Where(x => x.Id == 2).Sum(x => x.Total * x.Amount)
                      - this.Products.FirstOrDefault(x => x.Id == 2)!.Amount;
        }
        else if (checkDiscount)
        {
            foreach (var key in Discounts)
            {
                var discount = key.Value;
                if (discount.IsPercentage)
                {
                    var limit = key.Value.LimitAmount;

                    if (amount > limit)
                    {
                        amount -= key.Value.LimitValue;
                    }
                    else
                    {
                        double discountPercent = (key.Value.Value / 100);
                        amount *= discountPercent;
                    }
                }
                else
                {
                    amount -= key.Value.Value;
                }
            }
        }
        else
        {
            return amount;
        }


        return amount;
    }

    public void SetDiscount(Cart cart, string name, Discount discount)
    {
        cart.Discounts.Add(name, discount);
    }

    public bool CheckDiscount()
    {
        return Discounts.Count > 0;
    }

    public bool CheckDiscountByName(string name)
    {
        return Discounts.ContainsKey(name);
    }

    public void RemoveDiscountByName(string name)
    {
        Discounts.Remove(name);
    }

    public bool IsEmpty()
    {
        return !Products.Any();
    }

    public bool IsProductExists(int productId)
    {
        return Products.Any(x => x.Id == productId);
    }

    public List<Product> ListAll()
    {
        return Products;
    }

    public int ListUniqueProduct()
    {
        return Products.Count;
    }

    public int ListAllProduct()
    {
        return Products.Sum(x => x.Total);
    }

    public void Dispose()
    {
    }

    public void SetProductWithFreebie(List<Product> products)
    {
        Products.AddRange(products);

        if (HasFreebie())
        {
            Products.Add(new Product()
            {
                Id = 2,
                Total = 1,
                Amount = 100
            });
        }

        Products.AddRange(products);
    }
}

public class Discount
{
    public bool IsPercentage { get; set; }
    public int LimitValue { get; set; }
    public int Value { get; set; }
    public int LimitAmount { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public int Total { get; set; }
    public int Amount { get; set; }
}