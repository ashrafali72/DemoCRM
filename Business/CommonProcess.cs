using Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Business
{
    public class CommonProcess
    {
        private readonly IConfiguration _configuration;
        public CommonProcess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Get Party By ID
        public async Task<Dictionary<int,string>> GetAllParty(string Name)
         {
            Dictionary<int, string> parties = new();
            string query = "select id,MarketedBy from party where IsActive=1 and MarketedBy like '%" + Name + "%'";
            try
            {
                DataTable Dt = new();
                SqlCommand cmd = new();
                using (SqlConnection con = new(_configuration.GetConnectionString("myCon")))
                {
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataAdapter Da = new(cmd);
                    Da.Fill(Dt);
                }
                parties= Dt.AsEnumerable()
                    .ToDictionary<DataRow, int, string>(row => row.Field<int>(0),
                               row => row.Field<string>(1));
            }
            catch (Exception ex)
            { 
            }
            return await Task.FromResult<Dictionary<int, string>>( parties);
        }
        #endregion

        #region Get Print Order Details
        public List<PrintOrderClass> GetPrintOrder(string partyOrBrand, string type, string from, string to)
        {
           
            List<PrintOrderClass> printOrderClasses = new List<PrintOrderClass>();
            string daterange = "";
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                daterange = " and (convert(date, odn.created) between '" + from + "' and '" + to + "')";

            string query = "";
            if (type == "brand")
            {
                query = @"SELECT top(5000) odn.Id,odn.OrderCode,odn.BrandName,odn.LabelClaim,odn.Created,odn.OrderType,odn.Quantity,odn.Rate,odn.MRP,odn.TabCapType,odn.TabCapSize,
                            odn.PackType,odn.PackSize,(ISNULL(odn.CylinderCharge, 0.00)) CylinderCharge,(ISNULL(odn.ApprovalCharge, 0.00)) ApprovalCharge,(ISNULL(odn.ItemSecurity, 0.00)) ItemSecurity,odn.AcceptedDate,odn.Comments,odn.Remarks,odn.AssignTo,odn.PvcColor, 
                            p.PaymentType,ISNULL(b.ExpiryMonth, 0) ExpiryMonth,od.Rate [PreRate],isnull(od.Quantity,0) [PreQuantity]
                            FROM ordernew odn
                            left JOIN party p ON odn.partyid = p.id
                            left JOIN [dbo].[user] u ON u.id = p.associateid
                            left JOIN bomheader b ON b.id = CONVERT(FLOAT, ISNULL(dbo.fnShowOnlyFloatAndNumbers(odn.bomid), 0)) outer apply  (
                                    SELECT TOP 1 Rate,Quantity
                                    FROM OrderNew
                                    WHERE Brandname = odn.brandname
                                    AND id < odn.id
                                    AND statusid IN (13, 35)
                                    AND isactive = 1
                                    ORDER BY id DESC) as od
                            where odn.isactive=1 and odn.IsFreeZe != 1 and odn.BrandName like '" + partyOrBrand + "%' " + daterange + " ORDER BY odn.id DESC";
            }
            else
            {
                query = @"SELECT top(5000) odn.Id,odn.OrderCode,odn.BrandName,odn.LabelClaim,odn.Created,odn.OrderType,odn.Quantity,odn.Rate,odn.MRP,odn.TabCapType,odn.TabCapSize,
                    odn.PackType,odn.PackSize,(ISNULL(odn.CylinderCharge, 0.00)) CylinderCharge,(ISNULL(odn.ApprovalCharge, 0.00)) ApprovalCharge,(ISNULL(odn.ItemSecurity, 0.00)) ItemSecurity,odn.AcceptedDate,odn.Comments,odn.Remarks,odn.AssignTo,odn.PvcColor, 
                    p.PaymentType,ISNULL(b.ExpiryMonth, 0) ExpiryMonth,od.Rate [PreRate],isnull(od.Quantity,0) [PreQuantity]
                    FROM ordernew odn
                    left JOIN party p ON odn.partyid = p.id
                    left JOIN [dbo].[user] u ON u.id = p.associateid
                    left JOIN bomheader b ON b.id = CONVERT(FLOAT, ISNULL(dbo.fnShowOnlyFloatAndNumbers(odn.bomid), 0)) outer apply  (
                            SELECT TOP 1 Rate,Quantity
                            FROM OrderNew
                            WHERE Brandname = odn.brandname
                            AND id < odn.id
                            AND statusid IN (13, 35)
                            AND isactive = 1
                            ORDER BY id DESC) as od
                    where odn.isactive=1 and odn.IsFreeZe != 1 and  odn.UserOrganisation = '" + partyOrBrand + "' " + daterange + " ORDER BY odn.id DESC";
            }
            try
            {
                DataTable Dt = new();
                SqlCommand cmd = new();
                using (SqlConnection con = new(_configuration.GetConnectionString("myCon")))
                {
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataAdapter Da = new(cmd);
                    Da.Fill(Dt);
                }
                printOrderClasses = Dt.AsEnumerable()
                                .Select(dataRow => new PrintOrderClass
                                {
                                    Id = dataRow.Field<int>("Id"),
                                    OrderCode = dataRow.Field<string>("OrderCode"),
                                    OrderType = dataRow.Field<string>("OrderType"),
                                    BrandName = dataRow.Field<string>("BrandName"),
                                    LabelClaim = dataRow.Field<string>("LabelClaim"),
                                    Rate = dataRow.Field<string>("Rate"),
                                    MRP = dataRow.Field<string>("MRP"),
                                    PackType = dataRow.Field<string>("PackType"),
                                    PackSize = dataRow.Field<string>("PackSize"),
                                    TabCapType = dataRow.Field<string>("TabCapType"),
                                    TabCapSize = dataRow.Field<string>("TabCapSize"),
                                    CylinderCharge = dataRow.Field<decimal>("CylinderCharge"),
                                    ApprovalCharge = dataRow.Field<decimal>("ApprovalCharge"),
                                    ItemSecurity = dataRow.Field<decimal>("ItemSecurity"),
                                    Comments = dataRow.Field<string>("Comments"),
                                    Remarks = dataRow.Field<string>("Remarks"),
                                    AssignTo = dataRow.Field<string>("AssignTo"),
                                    PvcColor = dataRow.Field<string>("PvcColor"),
                                    PaymentType = dataRow.Field<string>("PaymentType"),
                                    ExpiryMonth = dataRow.Field<int>("ExpiryMonth"),
                                    //AcceptedDate = dataRow.Field<DateTime>("AcceptedDate").Equals(DBNull.Value) ? DateTime.Now : dataRow.Field<DateTime>("AcceptedDate"),
                                    AcceptedDate =  DateTime.Now,
                                    Created = dataRow.Field<DateTime>("Created"),
                                    PreQuantity = dataRow.Field<int>("PreQuantity"),
                                    PreRate = dataRow.Field<string>("PreRate"),

                                }).ToList();
            }
            catch (Exception ex)
            {
                
            }
            return printOrderClasses;
        }
        #endregion
    }
}
