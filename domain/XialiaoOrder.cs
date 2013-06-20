using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    public class XialiaoOrder
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
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
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
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private LinkedList<XialiaoOrderItem> xialiaoOrderItems = new LinkedList<XialiaoOrderItem>();

        internal LinkedList<XialiaoOrderItem> XialiaoOrderItems
        {
            get { return xialiaoOrderItems; }
            set { xialiaoOrderItems = value; }
        }
        private LinkedList<XialiaoOrderStats> xialiaoOrderstats = new LinkedList<XialiaoOrderStats>();

        internal LinkedList<XialiaoOrderStats> XialiaoOrderstats
        {
            get { return xialiaoOrderstats; }
            set { xialiaoOrderstats = value; }
        }

        private decimal totalNumber;

        public decimal TotalNumber
        {
            get { return totalNumber; }
            set { totalNumber = value; }
        }

        public XialiaoOrder()
        {
            sn = "";
            id = 0;
        }

        public XialiaoOrder(int id)
        {
            sn = ""; 
            this.id = id;
        }
    }
}
