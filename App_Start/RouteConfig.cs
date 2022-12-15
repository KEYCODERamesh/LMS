using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HospitalManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {



            /*---Attribute Routing definesEither Attribute will be used or custome will be used if you will use Attribute routing must call below line here 

            routes.MapMvcAttributeRoutes();*/
            /*---Custom router will always called before default route it is also working it example of custoem conventional routing */

        //    routes.MapRoute(
        //    "ArtistImages",                                              // Route name
        //    "{controller}/{action}/{artistName}/{apikey}",                           // URL with parameters
        //    new { controller = "MyTestList", action = "AddTestParameterParameter", artistName = "", apikey = "" }  // Parameter defaults
        //);



            routes.MapRoute(
           name: "PurchaseDetails",
           url: "{controller}/{action}/{id}",
           defaults:
            new { controller = "LabPurchaseDTs", action = "Create", id = UrlParameter.Optional }
       );



            routes.MapRoute(
             name: "StockEntry",
             url: "{controller}/{action}/{id}",
             defaults:
              new { controller = "StockEntry", action = "ShowVendor", id = UrlParameter.Optional }
         );


            routes.MapRoute(
             name: "Referals",
             url: "{controller}/{action}/{id}",
             defaults:
              new { controller = "Referal", action = "Index", id = UrlParameter.Optional }
         );



            routes.MapRoute(
               name: "Test",
               url: "{controller}/{action}/{id}",
               defaults:
                new { controller = "LabTestData", action = "AddTest", id = UrlParameter.Optional }
           );

           
            routes.MapRoute(
               name: "TestGroup",
               url: "{controller}/{action}/{id}",
               defaults:
                new { controller = "TestGroup", action = "ListGroups", id = UrlParameter.Optional }
           );


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: 
                 new { controller = "Home", action = "Login", id = UrlParameter.Optional }                
            );


           


        }
    }
}
