using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public Order()
    {
       
    }
    public int id { get; set; }
    public double total_bill { get; set; }
    public string content { get; set; }
    public string status_payment { get; set; }
    public string status_delivery { get; set; }
    public string status_bill { get; set; }
    public DateTime? date { get; set; }
}