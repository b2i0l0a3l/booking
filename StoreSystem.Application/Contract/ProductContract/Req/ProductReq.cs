using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;
using StoreSystem.Core.enums;

namespace BookingSystem.Application.Contract.ProductContract.Req
{
    public record ProductReq 
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? SKU { get; set; }
        public DateTime UpdateAt{ get; set; }

        public string? Barcode { get; set; }

        public decimal CostPrice { get; set; }
        private UnitType _Unit { get; set; }           

        public string Unit {
            get
            {
                switch (_Unit)
                {
                    case UnitType.pcs:
                        return "pcs";
                    case UnitType.kg:
                        return "Kg";
                    case UnitType.ltr:
                        return "Liter";


                    default:
                        return "Pack";
                }

            }
             set
            {
                if (Enum.TryParse(value, true, out UnitType parsed))
                    _Unit = parsed;
                else
                    _Unit = UnitType.pack;
            } }           

        public decimal SellPrice { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public int StoreId { get; set; }

    }
}