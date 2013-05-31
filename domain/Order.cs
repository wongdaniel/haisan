using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    public class Order
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string sn;

        public string Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private Company company;

        internal  Company Company
        {
            get { return company; }
            set { company = value; }
        }
        private string wayOfPayment;

        public string WayOfPayment
        {
            get { return wayOfPayment; }
            set { wayOfPayment = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private DateTime createDate;

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        private User operatr;

        public User Operatr
        {
            get { return operatr; }
            set { operatr = value; }
        }
        private decimal totalNumber;

        public decimal TotalNumber
        {
            get { return totalNumber; }
            set { totalNumber = value; }
        }
        private int totalPackages;

        public int TotalPackages
        {
            get { return totalPackages; }
            set { totalPackages = value; }
        }
        private decimal payment;

        public decimal Payment
        {
            get { return payment; }
            set { payment = value; }
        }
        private decimal processingCharges;

        public decimal ProcessingCharges
        {
            get { return processingCharges; }
            set { processingCharges = value; }
        }
        private decimal totalCost;

        public decimal TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }
        private decimal advancesReceived;

        public decimal AdvancesReceived
        {
            get { return advancesReceived; }
            set { advancesReceived = value; }
        }

        private LinkedList<OrderItem> orderItems = new LinkedList<OrderItem>();

        internal LinkedList<OrderItem> OrderItems
        {
            get { return orderItems; }
            set { orderItems = value; }
        }

        private LinkedList<OrderStats> orderStats = new LinkedList<OrderStats>();

        internal LinkedList<OrderStats> OrderStats
        {
            get { return orderStats; }
            set { orderStats = value; }
        }

        public Order(int id)
        {
            this.id = id;
        }

        public Order()
        {
        }
    }
}
