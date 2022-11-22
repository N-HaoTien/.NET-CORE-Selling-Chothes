using ClothingShopping.Extension;
using ClothingShopping.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayPal.Api;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System.Net;
using Item = PayPal.Api.Item;
using Payer = PayPal.Api.Payer;
using ClothingShopping.Models;
using ClothingShopping.Services;
using Microsoft.AspNetCore.Identity;
using Order = ClothingShopping.Models.Order;

namespace ClothingShopping.Controllers
{
    public class PaymentController : Controller
    {
        static String clientId = "ARet3hh2cJSQDv2j6dEhHKYrMfMc44i5L3sCGXyuz1xWan7P_4Y5FYRs-voXqGyjGfRTjzNBeDUKEsSu";
        static String secret = "EHtmP3kbJZT9W81g2PvYJLqwUw2MldMCgaboT8dbhRENdp40I_xe0XDCyFTN3wd-HY5o-Gvi9cjTh6kJ";
        public double TyGiaUSD = 23000;
        private PayPal.Api.Payment payment;
        public List<CartViewModel> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CartController.CartKey);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartViewModel>>(jsoncart);
            }
            return new List<CartViewModel>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(CheckoutViewModel ckViewModel)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Query["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = HttpContext.Request.Scheme + "://" + Request.Host.Value + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    HttpContext.Session.SetString(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Query["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, HttpContext.Session.GetString(guid) as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("Success", "Cart", new { mess = "Error" });
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Success", "Cart", new
                {
                    mess = ex.Message
                });
            }

            return RedirectToAction("Success", "Cart", new { mess = "Success",model = ckViewModel });
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apicontext, string redirectURl)
        {
            var jsoncart = HttpContext.Session.GetString(CartController.CartKey);
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            if (HttpContext.Session.GetString(CartController.CartKey) != null)
            {
                var d = GetCurrencyExchange("VND", "USD");
                foreach (var item in GetCartItems())
                {
                    decimal p = Math.Round(int.Parse(item.Price.ToString()) * d, 0);
                    itemList.items.Add(new Item()
                    {
                        name = item.Name,
                        currency = "USD",
                        price = p.ToString(),
                        quantity = item.Quantity.ToString(),
                        sku = "sku"
                    });
                }

                var payer = new Payer()
                {
                    payment_method = "paypal"
                };

                var redirUrl = new RedirectUrls()
                {
                    cancel_url = redirectURl + "&Cancel=true",
                    return_url = redirectURl
                };

                var details = new Details()
                {
                    tax = "0",
                    shipping = "0",
                    subtotal = itemList.items.Sum(x => int.Parse(x.price) * int.Parse(x.quantity)).ToString()
                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = details.subtotal,
                    details = details
                };

                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = Convert.ToString((new Random()).Next(100000)),
                    amount = amount,
                    item_list = itemList
                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrl
                };
            }

            return this.payment.Create(apicontext);
        }

        public Decimal GetCurrencyExchange(String localCurrency, String foreignCurrency)
        {
            var code = $"{localCurrency}_{foreignCurrency}";
            var newRate = FetchSerializedData(code);
            return newRate;
        }

        private Decimal FetchSerializedData(string code)
        {
            var url = "https://free.currconv.com/api/v7/convert?q=" + code + "&compact=y&apiKey=7cf3529b5d3edf9fa798";
            var webClient = new WebClient();
            var jsonData = String.Empty;

            var conversionRate = 1.0m;
            try
            {
                jsonData = webClient.DownloadString(url);
                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(jsonData);
                var result = jsonObject[code];
                conversionRate = result["val"];

            }
            catch (Exception) { }

            return conversionRate;
        }
    }
}
