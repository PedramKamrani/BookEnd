using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookEnd.Migrations;
using BookEnd.Models;
using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZarinpalSandbox;
using Order = BookEnd.Models.Order;

namespace BookEnd.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly BookContext _context;
        public OrdersController(BookContext context)
        {
            _context = context;
        }
        
        public IActionResult Order(int id)
        {
            var CurentUserId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Order = _context.Orders.SingleOrDefault(o => o.UserId == CurentUserId&&!o.IsFainaly);
            if (Order == null)
            {
                var details = new Order
                {
                    CreateDate = DateTime.Now,
                    Sum=0,
                    IsFainaly=false,
                    UserId=CurentUserId, 
                };
                _context.Add(details);
                _context.SaveChanges();
                _context.Add(new OrdeeDetails
                {
                    OrderId=details.OrderId,
                    Price=_context.BookStors.Find(id).Price,
                    Count=1,
                    BookId=id
                });
                _context.SaveChanges();
                SumOrder(details.OrderId);
            }
            else
            {
                var Detail = _context.OrdeeDetails.SingleOrDefault(d => d.OrderId == Order.OrderId && d.BookId == id);
                if (Detail == null)
                {
                    _context.Add(new OrdeeDetails
                    {
                        OrderId = Order.OrderId,
                        Price = _context.BookStors.Find(id).Price,
                        Count = 1,
                        BookId = id
                    });
                   
                }
                else
                {
                    Detail.Count += 1;
                    _context.Update(Detail);
                    SumOrder(Detail.OrderId);

                }
                _context.SaveChanges();
            }

            
            return RedirectToAction("ShowOrder");
        }

        public void SumOrder(int Id)
        {
            var order = _context.Orders.Find(Id);
            order.Sum = _context.OrdeeDetails.Where(o =>o.OrderId==order.OrderId).Select(d=>d.Count*d.Price).Sum();
            _context.Update(order);
            _context.SaveChanges();
        }

        public IActionResult ShowOrder()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

          var order = _context.Orders.SingleOrDefault(o => o.UserId == userid && o.IsFainaly == false);
            List<ShowOrder> _list = new List<ShowOrder>();
            if (order != null)
            {
                var detal = _context.OrdeeDetails.Where(o => o.OrderId== order.OrderId).ToList();
                foreach (var item in detal)
                {
                    var product = _context.BookStors.Find(item.BookId);
                    _list.Add(new ShowOrder
                    {
                        Count = item.Count,
                        Price = item.Price,
                        ProductName = product.Title,
                        OrderDetailId = item.OrderDetails,
                        Sum = item.Price * item.Count,
                        IsFainaly = order.IsFainaly
                    });
                }
            }
            return View(_list);
        }

        public IActionResult Delete(int id)
        {
            var det = _context.OrdeeDetails.Find(id);
            _context.Remove(det);
            _context.SaveChanges();
            return RedirectToAction("ShowOrder");
        }

        public IActionResult Command(int id, string command)
        {

            var det = _context.OrdeeDetails.Find(id);
            switch (command)
            {
                case "up":
                    {
                        det.Count += 1;
                        _context.Update(det);
                        break;
                    }
                case "down":
                    {
                        det.Count -= 1;
                        if (det.Count == 0)
                        {
                            _context.OrdeeDetails.Remove(det);
                        }
                        else
                        {
                            _context.Update(det);
                        }
                        break;
                    }
            }

            _context.SaveChanges();
            UpdateSumOrder(det.OrderId);
            return RedirectToAction("ShowOrder");
        }


        public IActionResult AddToCart(int id)
        {
            var userid =User.FindFirst("userid").Value;

            var order = _context.Orders.SingleOrDefault(o => o.UserId==userid&& o.IsFainaly == false);
            if (order == null)
            {
                int orderid = AddOrder(int.Parse(userid));
                bool res = AddOrderDetaile(orderid, id);
                if (true)
                    UpdateSumOrder(orderid);
                return Redirect("/Home/Detail");
            }
            else
            {

                var res = _context.OrdeeDetails
                 .SingleOrDefault(o => o.OrderId == order.OrderId && o.BookId == id);
                if (res != null)
                {
                    res.Count += 1;
                    _context.SaveChanges();
                    UpdateSumOrder(order.OrderId);

                }
                else
                {
                    AddOrderDetaile(order.OrderId, id);
                    _context.SaveChanges();
                }
                return RedirectToAction("ShowOrder");
            }
        }


        public void UpdateSumOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.Sum = _context.OrdeeDetails.Where(o => o.OrderId == order.OrderId).Sum(d => d.Price * d.Count);
            _context.Update(order);
            _context.SaveChanges();
        }

        public int AddOrder(int userid)
        {
            var order = new Order()
            {
                CreateDate = DateTime.Now,
                IsFainaly = false,
                UserId = userid.ToString()
            };
            _context.Add(order);
            _context.SaveChanges();
            return order.OrderId;
        }
        public bool AddOrderDetaile(int orderid, int productid)
        {
            int id = _context.BookStors.Find(productid).BookId;
            var detailes = new OrdeeDetails()
            {
                OrderId = orderid,
                Count = 1,
                Price = _context.BookStors.Find(id).Price,
                BookId = id
            };
            _context.Add(detailes);
            int res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }
        public IActionResult Payment()
        {
            var order = _context.Orders.SingleOrDefault(o => o.IsFainaly== false);
            if (order == null)
            {
                return NotFound();
            }
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payments = new Payment(order.Sum);
            var res = payments.PaymentRequest($"پرداخت{order.OrderId}", "https://localhost:44326/Orders/OnlinePayment/" + order.OrderId, "");
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }
        }



        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["status"] != "" &&
                HttpContext.Request.Query["status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string aturity = HttpContext.Request.Query["Authority"].ToString();
                var order = _context.Orders.Find(id);
                var payment = new Payment(order.Sum);
                var res = payment.Verification(aturity).Result;
                if (res.Status == 100)
                {
                    order.IsFainaly = true;
                    _context.Update(order);
                    _context.SaveChanges();
                    ViewBag.code = res.RefId;
                    return View();
                }
            }
            return View("ShowOrder");
        }
    }
}
    