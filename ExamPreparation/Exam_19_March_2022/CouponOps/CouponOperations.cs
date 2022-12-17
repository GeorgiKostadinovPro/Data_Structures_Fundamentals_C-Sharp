namespace CouponOps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using CouponOps.Models;
    using Interfaces;

    public class CouponOperations : ICouponOperations
    {
        private readonly IDictionary<string, Coupon> coupons;
        private readonly IDictionary<string, Website> websites;

        public CouponOperations()
        {
            this.coupons = new Dictionary<string, Coupon>();
            this.websites = new Dictionary<string, Website>();
        }   

        public void AddCoupon(Website website, Coupon coupon)
        {
            if (!this.Exist(website))
            {
                throw new ArgumentException();
            }

            this.coupons.Add(coupon.Code, coupon);
            website.Coupons.Add(coupon);
            coupon.Website = website;
        }

        public bool Exist(Website website)
        {
            return this.websites.ContainsKey(website.Domain);
        }

        public bool Exist(Coupon coupon)
        {
            return this.coupons.ContainsKey(coupon.Code);
        }

        public IEnumerable<Coupon> GetCouponsForWebsite(Website website)
        {
            if (!this.Exist(website))
            {
                throw new ArgumentException();
            }

            return website.Coupons;
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
        {
            return this.coupons.Values
                .OrderByDescending(c => c.Validity)
                .ThenByDescending(c => c.DiscountPercentage);
        }

        public IEnumerable<Website> GetSites()
        {
            return this.websites.Values;
        }

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
        {
            return this.GetSites()
                .OrderBy(w => w.UsersCount)
                .ThenByDescending(w => w.Coupons.Count);
        }

        public void RegisterSite(Website website)
        {
            if (this.websites.ContainsKey(website.Domain))
            {
                throw new ArgumentException();
            }

            this.websites.Add(website.Domain, website);
        }

        public Coupon RemoveCoupon(string code)
        {
            if (!this.coupons.ContainsKey(code))
            {
                throw new ArgumentException();
            }

            Coupon coupon = this.coupons[code];
            this.coupons.Remove(code);

            Website website = this.GetSites().FirstOrDefault(w => w.Coupons.Contains(coupon));
            website.Coupons.Remove(coupon);

            return coupon;
        }

        public Website RemoveWebsite(string domain)
        {
            if (!this.websites.ContainsKey(domain))
            {
                throw new ArgumentException();
            }

            Website website = this.websites[domain];
            this.websites.Remove(domain);

            foreach (var coupon in website.Coupons)
            {
                this.coupons.Remove(coupon.Code);
            }

            return website;
        }

        public void UseCoupon(Website website, Coupon coupon)
        {
            if (!this.Exist(website))
            {
                throw new ArgumentException();
            }

            if (!this.coupons.ContainsKey(coupon.Code))
            {
                throw new ArgumentException();
            }

            if (!website.Coupons.Contains(coupon))
            {
                throw new ArgumentException();
            }

            website.Coupons.Remove(coupon);
            this.coupons.Remove(coupon.Code);
        }
    }
}
