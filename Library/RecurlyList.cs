﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Recurly
{
    public class RecurlyList<T> : List<T>
    {

        /// <summary>
        ///  When paging
        /// </summary>
        private string _baseUrl;

        public RecurlyList()
            : base()
        {

        }

        public RecurlyList(string url)
            : base()
        {
            this._baseUrl = url;
        }

        private int _requestLimit = 50;

        /// <summary>
        /// TODO: implement 
        /// </summary>
        public int RequestLimit
        {
            get
            {
                return _requestLimit;
            }
            set
            {
                if (value <= 1 || value > 200)
                    throw new ArgumentOutOfRangeException("Request limit must be between 1 and 200.");
                else
                    _requestLimit = value;
            }
        }

       
       internal void ReadXml(XmlTextReader reader)
       {
           Type list = this.GetType().GetGenericArguments()[0];
           string element = "nothing";
           if (list == typeof(Adjustment))
               element = "line_items";
           else if (list == typeof(Transaction))
               element = "transactions";
           else if (list == typeof(Invoice))
               element = "invoices";
           else if (list == typeof(Plan))
               element = "plans";
           else if (list == typeof(AddOn))
               element = "add_ons";
           else if (list == typeof(Subscription))
               element = "subscriptions";
           else if (list == typeof(Account))
               element = "accounts";
           else if (list == typeof(Coupon))
               element = "coupons";

           while (reader.Read())
            {
                // End of account element, get out of here
                if (reader.Name.Equals(element) &&
                    reader.NodeType == XmlNodeType.EndElement)
                    break;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    { 
                        case "adjustment":
                            this.Add((T)(object)new Adjustment(reader));
                            break;

                        case "transaction":
                            this.Add((T)(object)new Transaction(reader));
                            break;

                        case "invoice":
                            this.Add((T)(object)new Invoice(reader));
                            break;

                        case "plan":
                            this.Add((T)(object)new Plan(reader));
                            break;

                        case "add_on":
                            this.Add((T)(object)new AddOn(reader));
                            break;

                        case "subscription":
                            this.Add((T)(object)new Subscription(reader));
                            break;

                        case "account":
                            this.Add((T)(object)new Account(reader));
                            break;

                        case "coupon":
                            this.Add((T)(object)new Coupon(reader));
                            break;

                    }
                }
            }


       }

    }
}
