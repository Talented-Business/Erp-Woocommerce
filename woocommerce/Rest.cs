using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce;
using WooCommerceNET.WooCommerce.v3;

namespace woocommerce
{
    public sealed class WC
    {
        private static volatile WC instance;
        private static RestAPI rest = new RestAPI("https://prodytec.shop/wp-json/wc/v3/", "ck_85ba24236b2c413ab410327a5bb389fadd6e702f", "cs_d99696445ae9b11808d9735a6a8f4d08e5e40f91");
        private static WCObject wc = new WCObject(rest);

        private WC() { }
        static WC() { }
        public static WC Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (wc)
                    {
                        if (instance == null)
                            instance = new WC();
                    }
                }

                return instance;
            }
        }
        static public WCObject getWC()
        {
            return wc;
        }

    }
}
